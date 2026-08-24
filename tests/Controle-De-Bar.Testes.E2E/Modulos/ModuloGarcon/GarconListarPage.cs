using System;
using Microsoft.Playwright;

namespace Controle_De_Bar.Testes.E2E.Modulos.ModuloGarcon;

public class GarconListarPage(
    IPage page,
    string urlBase
)
{
    public string Url => $"{urlBase}/Garcon/Listar";

    public ILocator Titulo => page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Listagem de Garçon" }
    );

    public ILocator CadastrarNovo => page.GetByRole(
        AriaRole.Link,
        new() { Name = "Cadastrar Novo" }
    );

    public ILocator EstadoVazio => page.GetByText(
        "Nenhum Garçon cadastrado.",
        new() { Exact = true }
    );

    public ILocator NomeDoGarcon(string nome)
    {
        return page.GetByText(
            nome,
            new() { Exact = true }
        );
    }

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task EditarAsync(string nome)
    {
        await CardPorNome(nome)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Editar", Exact = true }
            )
            .ClickAsync();
    }

    public async Task ExcluirAsync(string nome)
    {
        await CardPorNome(nome)
            .GetByRole(
                AriaRole.Link,
                new() { Name = "Excluir", Exact = true }
            )
            .ClickAsync();
    }

    private ILocator CardPorNome(string nome)
    {
        ILocator nomeGarcon = NomeDoGarcon(nome);

        return page.Locator(".card")
            .Filter(new() { Has = nomeGarcon });
    }
}
