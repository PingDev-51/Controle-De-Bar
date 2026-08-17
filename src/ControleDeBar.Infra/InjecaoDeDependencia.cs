using ControleDeBar.Infra.Compartilhado.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.Hosting;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Modulos.ModuloProduto;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Infra.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Infra.Modulos.ModuloGarcon;

namespace ControleDeBar.Infra;

public static class InjecaoDeDependencia
{
    public static void AddInfraRepositories(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging,
        IHostEnvironment environment
    )
    {
        // Injeta logs do Serilog
        Serilog.ILogger logger = SerilogFactory.Create(configuration, environment);

        logging.ClearProviders();

        services.AddSerilog(logger, dispose: true);

        // Injeta o DbContext do EF
        services.AddDbContext<ControleDeBarDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServerEF");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"A connection string \"SqlServerEF\" não foi encontrada."
                );
            }

            options.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(3);
            });
        });

        // Configuração do Usuário no Identity
        services.AddIdentityCore<IdentityUser<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>() // Configuração de Cargos/Papéis no Identity
        .AddEntityFrameworkStores<ControleDeBarDbContext>() // Integração com EntityFramework
        .AddSignInManager() // Configuração do SignInManager
        .AddDefaultTokenProviders();



        //Adicionar os modulos aqui <---------
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmOrm>();
        services.AddScoped<IRepositorioMesa, RepositorioMesaEmOrm>();
        services.AddScoped<IRepositorioGarcon, RepositorioGarconEmOrm>();
        // services.AddScoped<IRepositorioProva, RepositorioProvaEmOrm>();
    }
}
