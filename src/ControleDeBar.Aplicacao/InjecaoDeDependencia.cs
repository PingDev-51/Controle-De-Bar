using ControleDeBar.Aplicacao.Modulos.ModuloConta;
using ControleDeBar.Aplicacao.Modulos.ModuloGarcon;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.Aplicacao.Modulos.ModuloPedido;
using ControleDeBar.Aplicacao.Modulos.ModuloProduto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeBar.Aplicacao;

public static class InjecaoDeDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ServicoProduto>();
        services.AddScoped<ServicoMesa>();
        services.AddScoped<ServicoGarcon>();
        services.AddScoped<ServicoConta>();
        services.AddScoped<ServicoPedido>();

    }
}
