using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Infra.Modulos.ModuloContas;
using ControleDeBar.Infra.Modulos.ModuloMesa;
using ControleDeBar.Infra.Modulos.ModuloPedido;
using ControleDeBar.Infra.Modulos.ModuloProduto;
using Controle_De_Bar.Testes.Integracao.Compartilhado.Identity;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using GeradorDeProvas.Infra.Compartilhado.Orm;

namespace ControleDeBar.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioBaseEmOrmTests
{
    protected ControleDeBarDbContext dbContext = null!;

    protected RepositorioProdutoEmOrm repositorioProduto = null!;
    protected RepositorioMesaEmOrm repositorioMesa = null!;
    protected RepositorioContaEmOrm repositorioConta = null!;
    protected RepositorioPedidoEmOrm repositorioPedido = null!;

    protected Guid userId;

    [TestInitialize]
    public void InicializarContexto()
    {
        userId = Guid.NewGuid();

        dbContext = CriarDbContext(userId);

        repositorioProduto =
            new RepositorioProdutoEmOrm(dbContext);

        repositorioMesa =
            new RepositorioMesaEmOrm(dbContext);

        repositorioConta =
            new RepositorioContaEmOrm(dbContext);

        repositorioPedido =
            new RepositorioPedidoEmOrm(dbContext);

        ConfigurarNBuilder();
    }

    private void ConfigurarNBuilder()
    {
        BuilderSetup.SetCreatePersistenceMethod<Produto>(
            repositorioProduto.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<Mesa>(
            repositorioMesa.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<Conta>(
            repositorioConta.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<Pedido>(
            repositorioPedido.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Produto>>(
            produtos =>
            {
                foreach (Produto produto in produtos)
                    repositorioProduto.Cadastrar(produto);
            }
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Mesa>>(
            mesas =>
            {
                foreach (Mesa mesa in mesas)
                    repositorioMesa.Cadastrar(mesa);
            }
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Conta>>(
            contas =>
            {
                foreach (Conta conta in contas)
                    repositorioConta.Cadastrar(conta);
            }
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Pedido>>(
            pedidos =>
            {
                foreach (Pedido pedido in pedidos)
                    repositorioPedido.Cadastrar(pedido);
            }
        );
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
                .UseInMemoryDatabase(
                    $"ControleDeBarTestDB_{Guid.NewGuid():N}"
                )
                .Options;

        return new ControleDeBarDbContext(
            options,
            new ProvedorDeUsuarioFake(userId)
        );
    }
}