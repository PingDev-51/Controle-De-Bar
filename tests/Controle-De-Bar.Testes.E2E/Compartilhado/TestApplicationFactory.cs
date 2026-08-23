using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.WebApp;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Controle_De_Bar.Testes.E2E.Compartilhado;

public sealed class TestApplicationFactory : WebApplicationFactory<Entrypoint>
{

    private readonly string nomeBanco;
    protected InMemoryDatabaseRoot dbRoot;

    public string UrlBase { get; }

    public TestApplicationFactory()
    {
        nomeBanco = $"e2e-{Guid.NewGuid():N}";
        dbRoot = new InMemoryDatabaseRoot();

        UseKestrel(0);
        StartServer();

        UrlBase = ObterUrlKestrel();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.UseSetting(
            "Infra:NewRelic:Enabled",
            "False"
        );

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ControleDeBarDbContext>>();
            services.RemoveAll<IDbContextOptionsConfiguration<ControleDeBarDbContext>>();

            services.AddDbContext<ControleDeBarDbContext>(options =>
            {
                options.UseInMemoryDatabase(
                    nomeBanco,
                    dbRoot
                );
            });
        });
    }

    private string ObterUrlKestrel()
    {
        IServer servidor =
            Services.GetRequiredService<IServer>();

        IServerAddressesFeature? enderecos =
            servidor.Features.Get<IServerAddressesFeature>();

        if (enderecos is null)
            throw new InvalidOperationException(
                "Não foi possível obter a URL do servidor"
            );

        return enderecos.Addresses.Single();
    }
}