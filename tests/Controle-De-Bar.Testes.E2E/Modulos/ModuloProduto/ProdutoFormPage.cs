using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloProduto;

public sealed class ProdutoFormPage(
    IPage page,
    string urlBase
)
{
    public string UrlCadastrar => $"{urlBase}/Produto/Cadastrar";

    public string UrlEditar => $"{urlBase}/Produto/Editar";

    public ILocator Nome => page.GetByLabel("Nome");

    public ILocator Preco => page.GetByLabel("Preço");

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task IrParaEdicaoAsync(Guid id)
    {
        await page.GotoAsync($"{UrlEditar}/{id}");
    }

    public async Task PreencherAsync(
        string nome,
        decimal preco)
    {
        await Nome.FillAsync(nome.ToString());

        await Preco.FillAsync(
            preco.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
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