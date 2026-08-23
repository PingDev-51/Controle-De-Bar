using Controle_De_Bar.Testes.E2E.Compartilhado;
using Controle_De_Bar.Testes.E2E.Modulos.ModuloProduto;
using Microsoft.Playwright;

namespace ControleDeBar.Testes.E2E.Modulos.ModuloProduto;

[TestClass]
public sealed class ProdutoE2ETests : E2ETestsBase
{

    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemProdutos()
    {
        // Arrange
        await RegistrarEEntrarAsync("Produto.listagem@teste.local", "Senha123!");

        // Act
        await Page.GotoAsync($"{UrlBase}/Produto/Listar");

        // Assert
        Assert.AreEqual(
            "/Produto/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        // Heading = h1, h2, h3, h4, h5, h6
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Listagem de Produtos" }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Cadastrar Novo" }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("Nenhum produto cadastrado.", new() { Exact = true }))
            .ToBeVisibleAsync();
    }
    
    [TestMethod]
    public async Task DeveCadastrar_ProdutoComNomeEPrecoValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "produto.cadastro@teste.local",
            "Senha123!"
        );

        ProdutoFormPage formPage = new(Page, UrlBase);
        ProdutoListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            "Cerveja",
            10.00m
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NomeDoProduto("Cerveja")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.PrecoDoProduto("Cerveja", "10,00")
        ).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveEditar_ProdutoAlterandoPreco()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "produto.edicao@teste.local",
            "Senha123!"
        );

        await CadastrarProdutoAsync(
            "Cerveja",
            10.00m
        );

        ProdutoListarPage listarPage = new(Page, UrlBase);
        ProdutoFormPage formPage = new(Page, UrlBase);

        // Act
        await listarPage.EditarAsync("Cerveja");

        await formPage.PreencherAsync(
            "Cerveja",
            15.00m
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NomeDoProduto("Cerveja")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.PrecoDoProduto("Cerveja", "15,00")
        ).ToBeVisibleAsync();
    }

    private async Task CadastrarProdutoAsync(
        string nome,
        decimal preco)
    {
        ProdutoFormPage formPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            nome,
            preco
        );

        await formPage.ConfirmarAsync();

        ProdutoListarPage listarPage = new(Page, UrlBase);

        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);
    }
}