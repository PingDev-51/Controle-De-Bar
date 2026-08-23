using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;
using FizzWare.NBuilder;

namespace ControleDeBar.Testes.Integracao.Modulos.ModuloPedido;

[TestClass]
public sealed class RepositorioPedidoEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaRegistro()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Cerveja")
            .With(p => p.Preco = 10.00m)
            .Persist();

        Pedido pedido = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 2)
            .With(p => p.Total = 20.00m)
            .Build();

        // Ação
        repositorioPedido.Cadastrar(pedido);

        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado =
            repositorioPedido.SelecionarPorId(pedido.Id);

        // Asserção
        Assert.IsNotNull(pedidoSelecionado);
        Assert.AreEqual(pedido.Id, pedidoSelecionado.Id);
        Assert.AreEqual(conta.Id, pedidoSelecionado.ContaId);
        Assert.AreEqual(produto.Id, pedidoSelecionado.ProdutoId);
        Assert.AreEqual(2, pedidoSelecionado.Quantidade);
        Assert.AreEqual(20.00m, pedidoSelecionado.Total);
    }

    [TestMethod]
    public void Cadastrar_ComProdutoEQuantidadeValidos_CarregaRegistro()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Coca-Cola")
            .With(p => p.Preco = 8.00m)
            .Persist();

        Pedido pedido = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 1)
            .With(p => p.Total = 8.00m)
            .Build();

        // Ação
        repositorioPedido.Cadastrar(pedido);

        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado =
            repositorioPedido.SelecionarPorId(pedido.Id);

        // Asserção
        Assert.IsNotNull(pedidoSelecionado);
        Assert.AreEqual(produto.Id, pedidoSelecionado.ProdutoId);
        Assert.AreEqual(1, pedidoSelecionado.Quantidade);
        Assert.AreEqual(8.00m, pedidoSelecionado.Total);
    }

    [TestMethod]
    public void Cadastrar_ComQuantidadeMaiorQueUm_CarregaSubtotalCorretamente()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Cerveja")
            .With(p => p.Preco = 12.50m)
            .Persist();

        Pedido pedido = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 3)
            .With(p => p.Total = 37.50m)
            .Build();

        // Ação
        repositorioPedido.Cadastrar(pedido);

        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado =
            repositorioPedido.SelecionarPorId(pedido.Id);

        // Asserção
        Assert.IsNotNull(pedidoSelecionado);
        Assert.AreEqual(3, pedidoSelecionado.Quantidade);
        Assert.AreEqual(37.50m, pedidoSelecionado.Total);
    }

    [TestMethod]
    public void Editar_AtualizaRegistroExistente()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Cerveja")
            .With(p => p.Preco = 10.00m)
            .Persist();

        Pedido pedido = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 1)
            .With(p => p.Total = 10.00m)
            .Persist();

        Pedido pedidoAtualizado = Builder<Pedido>
            .CreateNew()
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 4)
            .With(p => p.Total = 40.00m)
            .Build();

        // Ação
        bool conseguiuEditar =
            repositorioPedido.Editar(
                pedido.Id,
                pedidoAtualizado
            );

        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado =
            repositorioPedido.SelecionarPorId(pedido.Id);

        // Asserção
        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(pedidoSelecionado);
        Assert.AreEqual(4, pedidoSelecionado.Quantidade);
        Assert.AreEqual(40.00m, pedidoSelecionado.Total);
    }

    [TestMethod]
    public void Excluir_RemoveRegistroExistente()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Cerveja")
            .With(p => p.Preco = 10.00m)
            .Persist();

        Pedido pedido = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 1)
            .With(p => p.Total = 10.00m)
            .Persist();

        // Ação
        bool conseguiuExcluir =
            repositorioPedido.Excluir(pedido.Id);

        dbContext.ChangeTracker.Clear();

        Pedido? pedidoSelecionado =
            repositorioPedido.SelecionarPorId(pedido.Id);

        // Asserção
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(pedidoSelecionado);
    }

    [TestMethod]
    public void SelecionarPorConta_CarregaPedidosDaConta()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto1 = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Cerveja")
            .With(p => p.Preco = 10.00m)
            .Persist();

        Produto produto2 = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Coca-Cola")
            .With(p => p.Preco = 8.00m)
            .Persist();

        Pedido pedido1 = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto1.Id)
            .With(p => p.Quantidade = 2)
            .With(p => p.Total = 20.00m)
            .Persist();

        Pedido pedido2 = Builder<Pedido>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto2.Id)
            .With(p => p.Quantidade = 3)
            .With(p => p.Total = 24.00m)
            .Persist();

        dbContext.ChangeTracker.Clear();

        // Ação
        IList<Pedido> pedidos =
            repositorioPedido.SelecionarPorConta(conta.Id);

        // Asserção
        Assert.HasCount(2, pedidos);

        Assert.IsTrue(
            pedidos.Any(p => p.Id == pedido1.Id)
        );

        Assert.IsTrue(
            pedidos.Any(p => p.Id == pedido2.Id)
        );
    }

    [TestMethod]
    public void SelecionarTodos_CarregaRegistros()
    {
        // Arranjo
        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Cliente Teste")
            .Persist();

        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Cerveja")
            .With(p => p.Preco = 10.00m)
            .Persist();

        IList<Pedido> pedidos = Builder<Pedido>
            .CreateListOfSize(3)
            .All()
            .With(p => p.UserId = userId)
            .With(p => p.ContaId = conta.Id)
            .With(p => p.ProdutoId = produto.Id)
            .With(p => p.Quantidade = 1)
            .With(p => p.Total = 10.00m)
            .Persist();

        dbContext.ChangeTracker.Clear();

        // Asserção
        Assert.HasCount(
            3,
            repositorioPedido.SelecionarTodos()
        );
    }
}