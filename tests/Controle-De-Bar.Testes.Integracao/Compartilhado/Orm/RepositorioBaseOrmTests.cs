using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Modulos.ModuloProduto;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Testes.Integracao.Compartilhado;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using Controle_De_Bar.Testes.Integracao.Compartilhado.Identity;
using ControleDeBar.Infra.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioBaseEmOrmTests
{
    protected ControleDeBarDbContext dbContext = null!;
    protected RepositorioProdutoEmOrm repositorioProduto = null!;
    protected RepositorioMesaEmOrm repositorioMesa = null!;

    protected Guid userId;

    [TestInitialize]
    public void InicializarContexto()
    {
        userId = Guid.NewGuid();

        dbContext = CriarDbContext(userId);

        // Produto
        repositorioProduto = new RepositorioProdutoEmOrm(dbContext);
        repositorioMesa = new RepositorioMesaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Produto>(
            repositorioProduto.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<Mesa>(
            repositorioMesa.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Produto>>((produtos) =>
        {
            foreach (Produto p in produtos)
                repositorioProduto.Cadastrar(p);
        });

        BuilderSetup.SetCreatePersistenceMethod<IList<Mesa>>((mesa) =>
        {
            foreach (Mesa m in mesa)
                repositorioMesa.Cadastrar(m);
        });
    }

    [TestCleanup]
    public void DescartarContexto()
    {
        dbContext.Dispose();
    }

    private static ControleDeBarDbContext CriarDbContext(Guid userId)
    {
        DbContextOptions<ControleDeBarDbContext> options =
            new DbContextOptionsBuilder<ControleDeBarDbContext>()
                .UseInMemoryDatabase("ControleDeBarTestDB_Memory")
                .Options;

        return new ControleDeBarDbContext(
            options,
            new ProvedorDeUsuarioFake(userId)
        );
    }
}
