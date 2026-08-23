using Controle_De_Bar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloConta;

[TestClass]
public sealed class ContaE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemContas()
    {
        // Arrange
        await RegistrarEEntrarAsync("Conta.listagem@teste.local", "Senha123!");

        // Act
        await Page.GotoAsync($"{UrlBase}/Contas/Listar");

        // Assert
        Assert.AreEqual(
            "/Contas/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        // Heading = h1, h2, h3, h4, h5, h6
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Listagem de Contas" }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Cadastrar Nova" }))
            .ToBeVisibleAsync();

        await Expect(Page.GetByText("Nenhuma conta cadastrada.", new() { Exact = true }))
            .ToBeVisibleAsync();
    }

    // [TestMethod]
    // public async Task DeveCadastrar_ContaComDadosValidos()
    // {
    //     // Arrange
    //     await RegistrarEEntrarAsync(
    //         "conta.cadastro@teste.local",
    //         "Senha123!"
    //     );

    //     // Cadastra o garçom necessário para a conta
    //     await CadastrarGarconAsync("João");

    //     // Cadastra a mesa necessária para a conta
    //     await CadastrarMesaAsync("10", "4");

    //     ContaFormPage formPage = new(Page, UrlBase);
    //     ContaListarPage listarPage = new(Page, UrlBase);

    //     // Act
    //     await formPage.IrParaCadastroAsync();

    //     await formPage.PreencherAsync(
    //         "Kauan",
    //         "João",
    //         "10"
    //     );

    //     await formPage.ConfirmarAsync();

    //     // Assert
    //     await Expect(Page)
    //         .ToHaveURLAsync(listarPage.Url);

    //     await Expect(
    //         listarPage.NomeDoCliente("Kauan")
    //     ).ToBeVisibleAsync();
    // }

    // private async Task CadastrarGarconAsync(string nome)
    // {
    //     GarconFormPage formPage = new(Page, UrlBase);

    //     await formPage.IrParaCadastroAsync();

    //     await formPage.PreencherNomeAsync(nome);

    //     await formPage.ConfirmarAsync();
    // }

    // private async Task CadastrarMesaAsync(
    //     string numero,
    //     string quantidadeLugares)
    // {
    //     MesaFormPage formPage = new(Page, UrlBase);

    //     await formPage.IrParaCadastroAsync();

    //     await formPage.PreencherAsync(
    //         numero,
    //         quantidadeLugares
    //     );

    //     await formPage.ConfirmarAsync();
    // }
}
