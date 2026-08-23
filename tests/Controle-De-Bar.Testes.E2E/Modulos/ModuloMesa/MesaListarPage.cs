using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloMesa;

public sealed class MesaListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Mesa/Listar";

    public ILocator Titulo => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Mesa" }
    );

    public ILocator CadastrarNovo => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Cadastrar Nova" }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhuma Mesa cadastrada.",
        new() { Exact = true }
    );

    public ILocator NumeroDaMesa(string numero)
    {
        return page.GetByText(
            $"Mesa N° {numero}",
            new() { Exact = true }
        );
    }

    public ILocator QuantidadeDeLugares(
        string numero,
        string quantidade)
    {
        ILocator card = CardPorNumero(numero);

        return card.GetByText(
            quantidade,
            new() { Exact = true }
        );
    }

    public ILocator StatusDaMesa(
        string numero,
        string status)
    {
        ILocator card = CardPorNumero(numero);

        return card.GetByText(
            status,
            new() { Exact = true }
        );
    }

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task EditarAsync(string numero)
    {
        await CardPorNumero(numero)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Editar", Exact = true }
            )
            .ClickAsync();
    }

    private ILocator CardPorNumero(string numero)
    {
        ILocator numeroMesa = NumeroDaMesa(numero);

        return page.Locator(".card")
            .Filter(new() { Has = numeroMesa });
    }
}
