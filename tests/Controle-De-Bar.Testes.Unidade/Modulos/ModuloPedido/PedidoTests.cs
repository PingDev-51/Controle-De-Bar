using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;

namespace Controle_De_Bar.Testes.Unidade.Modulos;

[TestClass]
public sealed class PedidoTests
{
    [TestMethod]
    public void AdicionarPedido_ComProdutoEQuantidadeValidos()
    {
        // Arrange
        Pedido pedido = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            2
        );

        // Act
        List<string> erros = pedido.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }

    [TestMethod]
    public void AdicionarPedido_SemProduto()
    {
        // Arrange
        Pedido pedido = new(
            Guid.NewGuid(),
            Guid.Empty,
            2
        );

        // Act
        List<string> erros = pedido.Validar();

        // Assert
        Assert.HasCount(1, erros);

        Assert.AreEqual(
            "O Produto precisa ser informado.",
            erros.First()
        );
    }

    [TestMethod]
    public void AdicionarPedido_ComQuantidadeNegativa()
    {
        // Arrange
        Pedido pedido = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            -1
        );

        // Act
        List<string> erros = pedido.Validar();

        // Assert
        Assert.HasCount(1, erros);

        Assert.AreEqual(
            "A Quantidade deve ser maior que zero.",
            erros.First()
        );
    }

    [TestMethod]
    public void AdicionarPedido_ECalcularTotal()
    {
        // Arrange
        Produto produto = new(
            "Cerveja",
            10
        );

        Pedido pedido = new(
            Guid.NewGuid(),
            produto.Id,
            3
        );

        pedido.Produto = produto;

        // Act
        pedido.CalcularTotal();

        // Assert
        Assert.AreEqual(
            30m,
            pedido.Total
        );
    }

    [TestMethod]
    public void AdicionarPedido_ComQuantidadeMaiorQueUm_CalculaSubtotal()
    {
        // Arrange
        Produto produto = new(
            "ColaCola",
            7.50m
        );

        Pedido pedido = new(
            Guid.NewGuid(),
            produto.Id,
            4
        );

        pedido.Produto = produto;

        // Act
        pedido.CalcularTotal();

        // Assert
        Assert.AreEqual(
            30m,
            pedido.Total
        );
    }

    [TestMethod]
    public void AdicionarPedido_SemConta()
    {
        // Arrange
        Pedido pedido = new(
            Guid.Empty,
            Guid.NewGuid(),
            2
        );

        // Act
        List<string> erros = pedido.Validar();

        // Assert
        Assert.HasCount(1, erros);

        Assert.AreEqual(
            "A Conta precisa ser informada.",
            erros.First()
        );
    }

    [TestMethod]
    public void AtualizarPedido_DeveAtualizarCampos()
    {
        // Arrange
        Pedido pedido = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1
        );

        Pedido pedidoAtualizado = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5
        );

        // Act
        pedido.Atualizar(pedidoAtualizado);

        // Assert
        Assert.AreEqual(
            pedidoAtualizado.ContaId,
            pedido.ContaId
        );

        Assert.AreEqual(
            pedidoAtualizado.ProdutoId,
            pedido.ProdutoId
        );

        Assert.AreEqual(
            5,
            pedido.Quantidade
        );
    }
}