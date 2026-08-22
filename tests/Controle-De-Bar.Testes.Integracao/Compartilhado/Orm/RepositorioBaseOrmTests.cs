using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Modulos.ModuloContas;
using ControleDeBar.Infra.Modulos.ModuloGarcon;
using ControleDeBar.Infra.Modulos.ModuloMesa;
using ControleDeBar.Infra.Modulos.ModuloPedido;
using ControleDeBar.Infra.Modulos.ModuloProduto;
using ControleDeBar.Testes.Integracao.Compartilhado.Identity;
using FizzWare.NBuilder;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioBaseEmOrmTests
{
    protected ControleDeBarDbContext dbContext = null!;

    protected RepositorioContaEmOrm repositorioConta = null!;
    protected RepositorioGarconEmOrm repositorioGarcon = null!;
    protected RepositorioMesaEmOrm repositorioMesa = null!;
    protected RepositorioPedidoEmOrm repositorioPedido = null!;
    protected RepositorioProdutoEmOrm repositorioProduto = null!;

    [TestInitialize]
    public void InicializarContexto()
    {
        Guid userId = Guid.NewGuid();

        DbContextOptions<ControleDeBarDbContext> options =
            new DbContextOptionsBuilder<ControleDeBarDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        dbContext = new ControleDeBarDbContext(
            options,
            new ProvedorDeUsuarioFake(userId)
        );


        // Conta
        repositorioConta = new RepositorioContaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Conta>(
            repositorioConta.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Conta>>((contas) =>
        {
            foreach (Conta c in contas)
                repositorioConta.Cadastrar(c);
        });


        // Garcon
        repositorioGarcon = new RepositorioGarconEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Garcon>(
            repositorioGarcon.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Garcon>>((garcons) =>
        {
            foreach (Garcon g in garcons)
                repositorioGarcon.Cadastrar(g);
        });


        // Mesa
        repositorioMesa = new RepositorioMesaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Mesa>(
            repositorioMesa.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Mesa>>((mesas) =>
        {
            foreach (Mesa m in mesas)
                repositorioMesa.Cadastrar(m);
        });


        // Pedido
        repositorioPedido = new RepositorioPedidoEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Pedido>(
            repositorioPedido.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Pedido>>((pedidos) =>
        {
            foreach (Pedido p in pedidos)
                repositorioPedido.Cadastrar(p);
        });


        // Produto
        repositorioProduto = new RepositorioProdutoEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Produto>(
            repositorioProduto.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Produto>>((produtos) =>
        {
            foreach (Produto p in produtos)
                repositorioProduto.Cadastrar(p);
        });
    }

    [TestCleanup]
    public void DescartarContexto()
    {
        dbContext.Dispose();
    }
}