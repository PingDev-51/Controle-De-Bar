using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloAutenticacao;

public class EntrarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Autenticacao/Entrar";

    public ILocator CampoEmail => page.GetByLabel("E-mail");

    public ILocator CampoSenha => page.GetByLabel("Senha");

    public ILocator LembrarMe => page.GetByLabel("Lembrar-me");

    public ILocator BotaoEntrar => page.GetByRole(
        AriaRole.Button,
        new() { Name = "Entrar", Exact = true }
    );

    public ILocator LinkCriarConta => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Criar Conta", Exact = true }
    );

    public ILocator MensagemErro => page.Locator(
        "[asp-validation-summary], .text-danger"
    );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task PreencherAsync(
        string email,
        string senha,
        bool lembrarMe = false)
    {
        await CampoEmail.FillAsync(email);
        await CampoSenha.FillAsync(senha);

        if (lembrarMe)
            await LembrarMe.CheckAsync();
    }

    public async Task ConfirmarAsync()
    {
        await BotaoEntrar.ClickAsync();
    }
}
