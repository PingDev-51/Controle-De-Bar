using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloConta;

public sealed class ContaListarPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public ContaListarPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url =>
        $"{urlBase}/Conta/Listar";

    public ILocator Conta(string nomeCliente)
    {
        return page
            .GetByRole(
                AriaRole.Heading,
                new()
                {
                    Name = nomeCliente,
                    Exact = true
                }
            );
    }

    public ILocator Garcon(string nomeCliente)
    {
        return ObterCard(nomeCliente)
            .GetByText(
                nomeCliente,
                new()
                {
                    Exact = false
                }
            );
    }

    public ILocator Situacao(
        string nomeCliente,
        string situacao)
    {
        return ObterCard(nomeCliente)
            .GetByText(
                situacao,
                new()
                {
                    Exact = true
                }
            );
    }

    public ILocator Mesa(
        string nomeCliente,
        string numeroMesa)
    {
        return ObterCard(nomeCliente)
            .GetByText(
                numeroMesa,
                new()
                {
                    Exact = true
                }
            );
    }

    public ILocator BotaoPedido(string nomeCliente)
    {
        return ObterCard(nomeCliente)
            .GetByRole(
                AriaRole.Link,
                new()
                {
                    Name = "Pedido"
                }
            );
    }

    private ILocator ObterCard(string nomeCliente)
    {
        return page
            .GetByRole(
                AriaRole.Heading,
                new()
                {
                    Name = nomeCliente,
                    Exact = true
                }
            )
            .Locator(
                "xpath=ancestor::div[contains(@class,'card')]"
            );
    }
}