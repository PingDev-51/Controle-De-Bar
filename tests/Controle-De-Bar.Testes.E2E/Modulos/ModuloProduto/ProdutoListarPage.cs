using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloProduto;

public sealed class ProdutoListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Produto/Listar";

    public ILocator Titulo => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Produtos" }
    );

    public ILocator CadastrarNovo => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Cadastrar Novo" }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhum produto cadastrado.",
        new() { Exact = true }
    );

    public ILocator NomeDoProduto(string nome) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = nome, Exact = true }
    );

    public ILocator PrecoDoProduto(string nome, string preco)
    {
        ILocator card = CardPorNome(nome);

        return card.GetByText(
            $"R$ {preco}",
            new() { Exact = true }
        );
    }

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task EditarAsync(string nome)
    {
        await CardPorNome(nome)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Editar", Exact = true }
            )
            .ClickAsync();
    }

    private ILocator CardPorNome(string nome)
    {
        ILocator nomeProduto = NomeDoProduto(nome);

        return page.Locator(".card")
            .Filter(new() { Has = nomeProduto });
    }
}