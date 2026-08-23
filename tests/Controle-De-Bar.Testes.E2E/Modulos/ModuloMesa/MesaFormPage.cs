using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloMesa;

public sealed class MesaFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Mesa/Cadastrar";

    public string UrlEditar(Guid id) =>
        $"{urlBase}/Mesa/Editar/{id}";

    public ILocator NumeroDaMesa =>
        page.GetByLabel("Numero da mesa");

    public ILocator QuantidadeDeLugares =>
        page.GetByLabel("Quantidade de lugares na mesa");

    public ILocator StatusDaMesa =>
        page.Locator("#StatusDaMesa");

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task IrParaEdicaoAsync(Guid id)
    {
        await page.GotoAsync(UrlEditar(id));
    }

    public async Task PreencherAsync(
        string numeroDaMesa,
        string quantidadeDeLugares,
        string statusDaMesa)
    {
        await NumeroDaMesa.FillAsync(numeroDaMesa);

        await QuantidadeDeLugares.FillAsync(
            quantidadeDeLugares
        );

        await StatusDaMesa.SelectOptionAsync(
            statusDaMesa
        );
    }

    public async Task ConfirmarAsync()
    {
        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Confirmar", Exact = true }
        ).ClickAsync();
    }
}
