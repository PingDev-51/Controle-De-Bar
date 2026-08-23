using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Compartilhado.Orm;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace Controle_De_Bar.Testes.E2E.Compartilhado;

public abstract class E2ETestsBase : PageTest
{
    private TestApplicationFactory aplicacao = null!;

    protected string UrlBase { get; set; } = string.Empty;

    [TestInitialize]
    public async Task InicializarAplicacao()
    {
        aplicacao = new TestApplicationFactory();

        UrlBase = aplicacao.UrlBase;
    }

    [TestCleanup]
    public async Task EncerrarAplicacao()
    {
        try
        {
            if (aplicacao is not null)
                await aplicacao.DisposeAsync();
        }
        finally
        {
            aplicacao = null!;
        }
    }

    protected async Task<Guid> RegistrarUsuarioAsync(string email, string senha)
    {
        using IServiceScope scope = aplicacao.Services.CreateScope();

        UserManager<IdentityUser<Guid>> userManager =
            scope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser<Guid>>>();

        IdentityUser<Guid> usuario = new IdentityUser<Guid>
        {
            Id = Guid.CreateVersion7(),
            UserName = email,
            Email = email
        };

        IdentityResult resultado =
            await userManager.CreateAsync(usuario, senha);

        Assert.IsTrue(
            resultado.Succeeded,
            string.Join(
                "; ",
                resultado.Errors.Select(erro => erro.Description)
            )
        );

        return usuario.Id;
    }

    protected async Task<Guid> RegistrarEEntrarAsync(string email, string senha)
    {
        Guid usuarioId = await RegistrarUsuarioAsync(email, senha);

        await Page.GotoAsync(
            $"{UrlBase}/Autenticacao/Entrar"
        );

        await Page.GetByLabel("E-mail").FillAsync(email);

        await Page.GetByLabel(
            "Senha",
            new() { Exact = true }
        ).FillAsync(senha);

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Entrar" }
        ).ClickAsync();

        return usuarioId;
    }

    protected async Task<Garcon> RegistrarGarconAsync(
        Guid usuarioId,
        string nome)
    {
        using IServiceScope scope = aplicacao.Services.CreateScope();

        ControleDeBarDbContext dbContext =
            scope.ServiceProvider
                .GetRequiredService<ControleDeBarDbContext>();

        Garcon garcon = new(nome)
        {
            UserId = usuarioId
        };

        dbContext.Add(garcon);

        await dbContext.SaveChangesAsync();

        return garcon;
    }

    protected async Task<Mesa> RegistrarMesaAsync(
        Guid usuarioId,
        string numeroDaMesa,
        string quantidadeDeLugares = "4")
    {
        using IServiceScope scope = aplicacao.Services.CreateScope();

        ControleDeBarDbContext dbContext =
            scope.ServiceProvider
                .GetRequiredService<ControleDeBarDbContext>();

        Mesa mesa = new(
            numeroDaMesa,
            quantidadeDeLugares,
            StatusDaMesa.Livre)
        {
            UserId = usuarioId
        };

        dbContext.Add(mesa);

        await dbContext.SaveChangesAsync();

        return mesa;
    }

    protected async Task<Produto> RegistrarProdutoAsync(
    Guid usuarioId,
    string nome,
    decimal preco)
    {
        using IServiceScope scope = aplicacao.Services.CreateScope();

        ControleDeBarDbContext dbContext =
            scope.ServiceProvider
                .GetRequiredService<ControleDeBarDbContext>();

        Produto produto = new(nome, preco)
        {
            UserId = usuarioId
        };

        dbContext.Add(produto);

        await dbContext.SaveChangesAsync();

        return produto;
    }
}