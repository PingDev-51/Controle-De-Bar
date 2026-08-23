using Controle_De_Bar.Testes.E2E.Compartilhado;
using Controle_De_Bar.Testes.E2E.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloProduto;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloPedido;

[TestClass]
public sealed class PedidoE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveCadastrar_PedidoComProdutoEQuantidadeValidos()
    {
        // Arrange
        Guid usuarioId = await RegistrarEEntrarAsync(
            "pedido.cadastro@teste.local",
            "Senha123!"
        );

        Produto produto = await RegistrarProdutoAsync(
            usuarioId,
            "Cerveja",
            10m
        );

        Guid contaId = await CriarContaAbertaAsync(usuarioId);

        PedidoFormPage formPage = new(Page, UrlBase);
        PedidoListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync(contaId);

        await formPage.PreencherAsync(
            produto.Id,
            1
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url(contaId));

        await Expect(
            listarPage.Produto("Cerveja")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Quantidade("Cerveja", 1)
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveCalcular_TotalDoPedidoAutomaticamente()
    {
        // Arrange
        Guid usuarioId = await RegistrarEEntrarAsync(
            "pedido.total@teste.local",
            "Senha123!"
        );

        Produto produto = await RegistrarProdutoAsync(
            usuarioId,
            "Cerveja",
            10m
        );

        Guid contaId = await CriarContaAbertaAsync(usuarioId);

        PedidoFormPage formPage = new(Page, UrlBase);
        PedidoListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync(contaId);

        await formPage.PreencherAsync(
            produto.Id,
            1
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(
            listarPage.Total("Cerveja", "R$ 10,00")
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveListar_TodosOsPedidosDaConta()
    {
        // Arrange
        Guid usuarioId = await RegistrarEEntrarAsync(
            "pedido.listagem@teste.local",
            "Senha123!"
        );

        Produto cerveja = await RegistrarProdutoAsync(
            usuarioId,
            "Cerveja",
            10m
        );

        Produto refrigerante = await RegistrarProdutoAsync(
            usuarioId,
            "Refrigerante",
            5m
        );

        Guid contaId = await CriarContaAbertaAsync(usuarioId);

        PedidoFormPage formPage = new(Page, UrlBase);
        PedidoListarPage listarPage = new(Page, UrlBase);

        // Primeiro pedido
        await formPage.IrParaCadastroAsync(contaId);

        await formPage.PreencherAsync(
            cerveja.Id,
            2
        );

        await formPage.ConfirmarAsync();

        // Segundo pedido
        await formPage.IrParaCadastroAsync(contaId);

        await formPage.PreencherAsync(
            refrigerante.Id,
            1
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(
            listarPage.Produto("Cerveja")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Quantidade("Cerveja", 2)
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Produto("Refrigerante")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Quantidade("Refrigerante", 1)
        ).ToBeVisibleAsync();
    }


    private async Task<Guid> CriarContaAbertaAsync(Guid usuarioId)
    {
        Garcon garcon = await RegistrarGarconAsync(
            usuarioId,
            "Garçom Teste"
        );

        Mesa mesa = await RegistrarMesaAsync(
            usuarioId,
            "1"
        );

        ContaFormPage contaFormPage = new(Page, UrlBase);
        ContaListarPage listarPage = new(Page, UrlBase);

        string nomeCliente = $"Cliente {Guid.NewGuid()}";

        await contaFormPage.IrParaCadastroAsync();

        await contaFormPage.PreencherAsync(
            nomeCliente,
            garcon.Nome,
            mesa.NumeroDaMesa
        );

        await contaFormPage.ConfirmarAsync();

        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        string? href = await listarPage
            .BotaoPedido(nomeCliente)
            .GetAttributeAsync("href");

        Assert.IsNotNull(href);

        Guid contaId = Guid.Parse(
            href.Split('/')[^1]
        );

        return contaId;
    }

    
}