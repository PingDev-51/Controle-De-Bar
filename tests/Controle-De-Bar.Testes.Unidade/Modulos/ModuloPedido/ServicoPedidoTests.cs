using ControleDeBar.Aplicacao.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using FluentAssertions;
using FluentResults;
using Moq;

namespace Controle_De_Bar.Testes.Unidade.Modulos;

[TestClass]
public sealed class ServicoPedidoTests
{
    [TestMethod]
    public void Cadastrar_DadosValidos_PersistePedido()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();

        Conta conta = new("Cliente Teste");

        Produto produto = new("Cerveja", 10);

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(contaId))
            .Returns(conta);

        repositorioProdutos
            .Setup(r => r.SelecionarPorId(produtoId))
            .Returns(produto);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado = servicoPedido.Cadastrar(
            new CadastrarPedidoDto(
                contaId,
                produtoId,
                2
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioPedido.Verify(
            r => r.Cadastrar(It.Is<Pedido>(p =>
                p.ContaId == contaId &&
                p.ProdutoId == produtoId &&
                p.Quantidade == 2 &&
                p.Total == 20
            )),
            Times.Once
        );
    }

    [TestMethod]
    public void Cadastrar_ContaNaoEncontrada_RetornaErro()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(contaId))
            .Returns((Conta?)null);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado = servicoPedido.Cadastrar(
            new CadastrarPedidoDto(
                contaId,
                produtoId,
                1
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "Conta não encontrada.",
            resultado.Errors.First().Message
        );

        repositorioPedido.Verify(
            r => r.Cadastrar(It.IsAny<Pedido>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_ProdutoNaoEncontrado_RetornaErro()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();

        Conta conta = new("Cliente Teste");

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(contaId))
            .Returns(conta);

        repositorioProdutos
            .Setup(r => r.SelecionarPorId(produtoId))
            .Returns((Produto?)null);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado = servicoPedido.Cadastrar(
            new CadastrarPedidoDto(
                contaId,
                produtoId,
                1
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "Produto não encontrado.",
            resultado.Errors.First().Message
        );

        repositorioPedido.Verify(
            r => r.Cadastrar(It.IsAny<Pedido>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_QuantidadeNegativa_RetornaErro()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();

        Conta conta = new("Cliente Teste");
        Produto produto = new("Cerveja", 10);

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(contaId))
            .Returns(conta);

        repositorioProdutos
            .Setup(r => r.SelecionarPorId(produtoId))
            .Returns(produto);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado = servicoPedido.Cadastrar(
            new CadastrarPedidoDto(
                contaId,
                produtoId,
                -1
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "A Quantidade deve ser maior que zero.",
            resultado.Errors.First().Message
        );

        repositorioPedido.Verify(
            r => r.Cadastrar(It.IsAny<Pedido>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_SemProduto_RetornaErro()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();

        Conta conta = new("Cliente Teste");

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(contaId))
            .Returns(conta);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado = servicoPedido.Cadastrar(
            new CadastrarPedidoDto(
                contaId,
                Guid.Empty,
                1
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "Produto não encontrado.",
            resultado.Errors.First().Message
        );

        repositorioPedido.Verify(
            r => r.Cadastrar(It.IsAny<Pedido>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_QuantidadeMaiorQueUm_CalculaSubtotalCorretamente()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();

        Conta conta = new("Cliente Teste");
        Produto produto = new("Cerveja", 12.50m);

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(contaId))
            .Returns(conta);

        repositorioProdutos
            .Setup(r => r.SelecionarPorId(produtoId))
            .Returns(produto);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado = servicoPedido.Cadastrar(
            new CadastrarPedidoDto(
                contaId,
                produtoId,
                3
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioPedido.Verify(
            r => r.Cadastrar(It.Is<Pedido>(p =>
                p.Quantidade == 3 &&
                p.Total == 37.50m
            )),
            Times.Once
        );
    }

    [TestMethod]
    public void SelecionarPorConta_DeveRetornarPedidosDaConta()
    {
        // Arrange
        Guid contaId = Guid.NewGuid();

        Produto produto1 = new("Cerveja", 10);
        Produto produto2 = new("Refrigerante", 8);

        Pedido pedido1 = new(
            contaId,
            produto1.Id,
            2
        )
        {
            Produto = produto1
        };

        Pedido pedido2 = new(
            contaId,
            produto2.Id,
            1
        )
        {
            Produto = produto2
        };

        pedido1.CalcularTotal();
        pedido2.CalcularTotal();

        Mock<IRepositorioPedido> repositorioPedido = new();

        repositorioPedido
            .Setup(r => r.SelecionarPorConta(contaId))
            .Returns([pedido1, pedido2]);

        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        List<ListarPedidoDto> resultado =
            servicoPedido.SelecionarPorConta(contaId);

        // Assert
        resultado.Should().HaveCount(2);

        resultado.Should().BeEquivalentTo(
            new List<ListarPedidoDto>
            {
                new(
                    pedido1.Id,
                    pedido1.ContaId,
                    pedido1.ProdutoId,
                    "Cerveja",
                    2,
                    20
                ),
                new(
                    pedido2.Id,
                    pedido2.ContaId,
                    pedido2.ProdutoId,
                    "Refrigerante",
                    1,
                    8
                )
            }
        );

        repositorioPedido.Verify(
            r => r.SelecionarPorConta(contaId),
            Times.Once
        );
    }

    [TestMethod]
    public void Excluir_PedidoCadastrado_DeveExcluirComSucesso()
    {
        // Arrange
        Pedido pedido = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1
        );

        Mock<IRepositorioPedido> repositorioPedido = new();
        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioProduto> repositorioProdutos = new();

        repositorioPedido
            .Setup(r => r.SelecionarPorId(pedido.Id))
            .Returns(pedido);

        ServicoPedido servicoPedido = new(
            repositorioPedido.Object,
            repositorioContas.Object,
            repositorioProdutos.Object
        );

        // Act
        Result resultado =
            servicoPedido.Excluir(pedido.Id);

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioPedido.Verify(
            r => r.Excluir(pedido.Id),
            Times.Once
        );
    }
}