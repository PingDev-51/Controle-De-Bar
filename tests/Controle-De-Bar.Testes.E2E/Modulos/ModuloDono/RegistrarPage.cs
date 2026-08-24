using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloAutenticacao;

public class RegistrarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Autenticacao/Registrar";

    public ILocator CampoEstabelecimento => page.GetByLabel(
        "Estabelecimento"
    );

    public ILocator CampoEmail => page.GetByLabel(
        "E-mail"
    );

    public ILocator CampoSenha => page.GetByLabel(
        "Senha",
        new() { Exact = true }
    );

    public ILocator CampoConfirmarSenha => page.GetByLabel(
        "Confirmar Senha"
    );

    public ILocator BotaoCriarConta => page.GetByRole(
        AriaRole.Button,
        new() { Name = "Criar Conta", Exact = true }
    );

    public ILocator LinkEntrar => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Entrar", Exact = true }
    );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task PreencherAsync(
        string estabelecimento,
        string email,
        string senha,
        string confirmarSenha)
    {
        await CampoEstabelecimento.FillAsync(estabelecimento);
        await CampoEmail.FillAsync(email);
        await CampoSenha.FillAsync(senha);
        await CampoConfirmarSenha.FillAsync(confirmarSenha);
    }

    public async Task ConfirmarAsync()
    {
        await BotaoCriarConta.ClickAsync();
    }
}
