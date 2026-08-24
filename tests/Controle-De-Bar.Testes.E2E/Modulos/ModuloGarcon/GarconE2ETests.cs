using System;
using Controle_De_Bar.Testes.E2E.Compartilhado;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloGarcon;

[TestClass]
public class GarconE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_ListagemVazia_ParaUsuario_SemGarcons()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "garcon.listagem@teste.local",
            "Senha123!"
        );

        GarconListarPage listarPage = new(Page, UrlBase);

        // Act
        await listarPage.IrParaAsync();

        // Assert
        Assert.AreEqual(
            "/Garcon/Listar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(
            listarPage.Titulo
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.CadastrarNovo
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.EstadoVazio
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveCadastrar_GarconComDadosValidos()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "garcon.cadastro@teste.local",
            "Senha123!"
        );

        GarconFormPage formPage = new(Page, UrlBase);
        GarconListarPage listarPage = new(Page, UrlBase);

        // Act
        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            "Osvaldo"
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NomeDoGarcon("Osvaldo")
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveEditar_GarconAlterandoNome()
    {
        // Arrange
        await RegistrarEEntrarAsync(
            "garcon.edicao@teste.local",
            "Senha123!"
        );

        await CadastrarGarconAsync("Osvaldo");

        GarconListarPage listarPage = new(Page, UrlBase);
        GarconFormPage formPage = new(Page, UrlBase);

        // Act
        await listarPage.EditarAsync("Osvaldo");

        await formPage.PreencherAsync(
            "Geraldo"
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);

        await Expect(
            listarPage.NomeDoGarcon("Geraldo")
        ).ToBeVisibleAsync();
    }


    private async Task CadastrarGarconAsync(
        string nome)
    {
        GarconFormPage formPage = new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            nome
        );

        await formPage.ConfirmarAsync();

        GarconListarPage listarPage = new(Page, UrlBase);

        await Expect(Page)
            .ToHaveURLAsync(listarPage.Url);
    }
}
