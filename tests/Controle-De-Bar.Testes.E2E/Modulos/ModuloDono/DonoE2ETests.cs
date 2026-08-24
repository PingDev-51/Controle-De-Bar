using System;
using Controle_De_Bar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloAutenticacao;

[TestClass]
public class AutenticacaoE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task DeveExibir_PaginaDeLogin()
    {
        // Arrange
        EntrarPage entrarPage = new(Page, UrlBase);

        // Act
        await entrarPage.IrParaAsync();

        // Assert
        Assert.AreEqual(
            "/Autenticacao/Entrar",
            new Uri(Page.Url).AbsolutePath
        );

        await Expect(entrarPage.CampoEmail)
            .ToBeVisibleAsync();

        await Expect(entrarPage.CampoSenha)
            .ToBeVisibleAsync();

        await Expect(entrarPage.BotaoEntrar)
            .ToBeVisibleAsync();

        await Expect(entrarPage.LinkCriarConta)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveExibir_ErroAoEntrar_ComCredenciaisInvalidas()
    {
        // Arrange
        EntrarPage entrarPage = new(Page, UrlBase);

        await entrarPage.IrParaAsync();

        // Act
        await entrarPage.PreencherAsync(
            "usuario.inexistente@teste.local",
            "SenhaErrada123!"
        );

        await entrarPage.ConfirmarAsync();

        // Assert
        await Expect(
            Page.GetByText(
                "E-mail ou senha inválidos.",
                new() { Exact = true }
            )
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveCriarConta_ComDadosValidos()
    {
        // Arrange
        string email =
            $"registro.{Guid.NewGuid():N}@teste.local";

        RegistrarPage registrarPage = new(Page, UrlBase);

        await registrarPage.IrParaAsync();

        // Act
        await registrarPage.PreencherAsync(
            "Bar Teste",
            email,
            "Senha123!",
            "Senha123!"
        );

        await registrarPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync($"{UrlBase}/");
    }


    [TestMethod]
    public async Task DeveExibir_ErroQuandoSenhasNaoConferem()
    {
        // Arrange
        RegistrarPage registrarPage = new(Page, UrlBase);

        await registrarPage.IrParaAsync();

        // Act
        await registrarPage.PreencherAsync(
            "Bar Teste",
            $"registro.{Guid.NewGuid():N}@teste.local",
            "Senha123!",
            "Senha456!"
        );

        await registrarPage.ConfirmarAsync();

        // Assert
        await Expect(
            Page.GetByText(
                "As senhas não conferem.",
                new() { Exact = true }
            )
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task DeveRedirecionar_ParaLoginAoAcessarPaginaProtegida()
    {
        // Act
        await Page.GotoAsync(
            $"{UrlBase}/Garcon/Listar"
        );

        // Assert
        Assert.AreEqual(
            "/Autenticacao/Entrar",
            new Uri(Page.Url).AbsolutePath
        );
    }
}
