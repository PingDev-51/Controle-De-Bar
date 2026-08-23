using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloPedido;

public class PedidoFormPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public PedidoFormPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public async Task IrParaCadastroAsync(Guid contaId)
    {
        await page.GotoAsync(
            $"{urlBase}/Pedido/Cadastrar?contaId={contaId}"
        );
    }

    public async Task SelecionarProdutoAsync(Guid produtoId)
    {
        await page
            .GetByLabel("Produto")
            .SelectOptionAsync(produtoId.ToString());
    }

    public async Task PreencherQuantidadeAsync(int quantidade)
    {
        await page
            .GetByLabel("Quantidade")
            .FillAsync(quantidade.ToString());
    }

    public async Task PreencherAsync(
        Guid produtoId,
        int quantidade)
    {
        await SelecionarProdutoAsync(produtoId);
        await PreencherQuantidadeAsync(quantidade);
    }

    public async Task ConfirmarAsync()
    {
        await page
            .GetByRole(
                AriaRole.Button,
                new() { Name = "Confirmar" }
            )
            .ClickAsync();
    }
}