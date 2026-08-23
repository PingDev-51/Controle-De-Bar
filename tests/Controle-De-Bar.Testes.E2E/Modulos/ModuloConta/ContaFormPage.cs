using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloConta;

public sealed class ContaFormPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public ContaFormPage(
        IPage page,
        string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url => $"{urlBase}/Conta/Cadastrar";

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task PreencherNomeClienteAsync(
        string nomeCliente)
    {
        await page
            .GetByLabel("Nome do Cliente")
            .FillAsync(nomeCliente);
    }

    public async Task SelecionarGarconAsync(
        string nomeGarcon)
    {
        await page
            .GetByLabel("Garçom")
            .SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = nomeGarcon
                }
            );
    }

    public async Task SelecionarMesaAsync(
        string numeroMesa)
    {
        await page
            .GetByLabel("Mesa")
            .SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = numeroMesa
                }
            );
    }

    public async Task PreencherAsync(
        string nomeCliente,
        string nomeGarcon,
        string numeroMesa)
    {
        await PreencherNomeClienteAsync(nomeCliente);

        await SelecionarGarconAsync(nomeGarcon);

        await SelecionarMesaAsync(numeroMesa);
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