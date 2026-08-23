using Controle_De_Bar.Testes.E2E.Compartilhado;
using Controle_De_Bar.Testes.E2E.Modulos.ModuloGarcon;
using Controle_De_Bar.Testes.E2E.Modulos.ModuloMesa;
using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloConta;

[TestClass]
public sealed class ContaE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemContas()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "conta.listagem@teste.local",
            "Senha123!"
        );

        // Act
        await Page.GotoAsync($"{UrlBase}/Conta/Listar");

        // Assert
        Assert.AreEqual(
            "/Conta/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(
            Page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Listagem de Contas" }
            )
        ).ToBeVisibleAsync();

        await Expect(
            Page.GetByRole(
                AriaRole.Link,
                new() { Name = "Cadastrar Nova" }
            )
        ).ToBeVisibleAsync();

        await Expect(
            Page.GetByText(
                "Nenhuma conta cadastrada.",
                new() { Exact = true }
            )
        ).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task DeveCadastrar_ContaComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "conta.cadastro@teste.local",
            "Senha123!"
        );

        // Garçom é uma dependência da Conta
        await CadastrarGarconAsync("João");

        // Mesa é uma dependência da Conta
        await CadastrarMesaAsync(
            "10",
            "4",
            "1"
        );

        ContaFormPage formPage = new(Page, UrlBase);
        ContaListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            "Kauan",
            "João",
            "10"
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

    }

    private async Task CadastrarGarconAsync(string nome)
    {
        GarconFormPage formPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherNomeAsync(nome);

        await formPage.ConfirmarAsync();
    }

    private async Task CadastrarMesaAsync(
        string numero,
        string quantidadeLugares,
        string statusDaMesa)
    {
        MesaFormPage formPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numero,
            quantidadeLugares,
            statusDaMesa
        );

        await formPage.ConfirmarAsync();
    }
}