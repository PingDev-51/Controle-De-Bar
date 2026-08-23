using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloPedido;

public class PedidoListarPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public PedidoListarPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url(Guid contaId)
        => $"{urlBase}/Pedido/Listar?contaId={contaId}";

    public ILocator Produto(string nomeProduto)
    {
        return page
            .GetByRole(AriaRole.Heading, new()
            {
                Name = nomeProduto,
                Exact = true
            });
    }

    public ILocator Quantidade(string nomeProduto, int quantidade)
    {
        ILocator card = page
            .GetByRole(AriaRole.Heading, new()
            {
                Name = nomeProduto,
                Exact = true
            })
            .Locator("xpath=ancestor::div[contains(@class,'card-body')]");

        return card.GetByText(
            quantidade.ToString(),
            new() { Exact = true }
        );
    }

    public ILocator Total(string nomeProduto, string total)
    {
        ILocator card = page
            .GetByRole(AriaRole.Heading, new()
            {
                Name = nomeProduto,
                Exact = true
            })
            .Locator("xpath=ancestor::div[contains(@class,'card-body')]");

        return card.GetByText(total);
    }
}