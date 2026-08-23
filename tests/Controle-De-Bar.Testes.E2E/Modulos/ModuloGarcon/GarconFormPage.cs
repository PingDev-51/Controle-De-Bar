using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloGarcon;

public sealed class GarconFormPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public GarconFormPage(
        IPage page,
        string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url => $"{urlBase}/Garcon/Cadastrar";

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task PreencherNomeAsync(
        string nome)
    {
        await page
            .GetByLabel("Nome do garçon")
            .FillAsync(nome);
    }

    public async Task PreencherAsync(
        string nome)
    {
        await PreencherNomeAsync(nome);
    }

    public async Task ConfirmarAsync()
    {
        await page
            .GetByRole(
                AriaRole.Button,
                new()
                {
                    Name = "Confirmar"
                }
            )
            .ClickAsync();
    }
}