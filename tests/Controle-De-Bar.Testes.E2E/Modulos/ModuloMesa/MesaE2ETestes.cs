using System;
using Controle_De_Bar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloMesa;

[TestClass]
public class MesaE2ETestes : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemMesas()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "mesa.listagem@teste.local",
            "Senha123!"
        );

        // Act
        await Page.GotoAsync($"{UrlBase}/Mesa/Listar");

        // Assert
        Assert.AreEqual(
            "/Mesa/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(
            Page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Listagem de Mesa" }
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
                "Nenhuma Mesa cadastrada.",
                new() { Exact = true }
            )
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveCadastrar_MesaComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "mesa.cadastro@teste.local",
            "Senha123!"
        );

        MesaFormPage formPage = new(Page, UrlBase);
        MesaListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            "1",
            "4",
            "1"
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NumeroDaMesa("1")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.QuantidadeDeLugares("1", "4")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.StatusDaMesa("1", "Livre")
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveEditar_MesaAlterandoQuantidadeDeLugares()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "mesa.edicao@teste.local",
            "Senha123!"
        );

        await CadastrarMesaAsync(
            "1",
            "4",
            "1"
        );

        MesaListarPage listarPage = new(Page, UrlBase);
        MesaFormPage formPage = new(Page, UrlBase);

        // Act
        await listarPage.EditarAsync("1");

        await formPage.PreencherAsync(
            "1",
            "6",
            "2"
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NumeroDaMesa("1")
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.QuantidadeDeLugares("1", "6")
        ).ToBeVisibleAsync();
    }


    private async Task CadastrarMesaAsync(
        string numeroDaMesa,
        string quantidadeDeLugares,
        string statusDaMesa)
    {
        MesaFormPage formPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            quantidadeDeLugares,
            statusDaMesa
        );

        await formPage.ConfirmarAsync();

        MesaListarPage listarPage = new(Page, UrlBase);

        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);
    }
}
