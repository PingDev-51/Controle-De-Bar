using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloMesa;

public class MesaListarPage(
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

    public ILocator Mesa(string numeroDaMesa) => page.GetByRole(
        AriaRole.Heading,
        new() { Name = $"Mesa N° {numeroDaMesa}", Exact = true }
    );

    public ILocator QuantidadeDeLugares(
        string numeroDaMesa,
        string quantidade
    )
    {
        ILocator card = CardPorMesa(numeroDaMesa);

        return card.GetByText(
            quantidade,
            new() { Exact = true }
        );
    }

    public ILocator StatusDaMesa(
        string numeroDaMesa,
        string status
    )
    {
        ILocator card = CardPorMesa(numeroDaMesa);

        return card.GetByText(
            status,
            new() { Exact = true }
        );
    }

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task EditarAsync(string numeroDaMesa)
    {
        await CardPorMesa(numeroDaMesa)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Editar", Exact = true }
            )
            .ClickAsync();
    }

    public async Task ExcluirAsync(string numeroDaMesa)
    {
        await CardPorMesa(numeroDaMesa)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Excluir", Exact = true }
            )
            .ClickAsync();
    }

    private ILocator CardPorMesa(string numeroDaMesa)
    {
        ILocator mesa = Mesa(numeroDaMesa);

        return page.Locator(".card")
            .Filter(new() { Has = mesa });
    }
}
