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
        // services.AddScoped<ServicoMateria>();
        // services.AddScoped<ServicoQuestao>();
        // services.AddScoped<ServicoProva>();
        // services.AddScoped<GeradorPdfProva>();

    }
}
