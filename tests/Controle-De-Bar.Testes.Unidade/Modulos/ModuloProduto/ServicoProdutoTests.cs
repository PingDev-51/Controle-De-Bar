using ControleDeBar.Aplicacao.Modulos.ModuloProduto;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using FluentAssertions;
using FluentResults;
using Moq;

namespace Controle_De_Bar.Testes.Unidade.Modulos;

[TestClass]
public sealed class ServicoProdutoTests
{
    [TestMethod]
    public void Cadastrar_DadosValidos_PersisteProduto()
    {
        //Arange
        Produto produto = new("testar", 10);

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto.Setup(r => r.SelecionarTodos()).Returns([]);

        Produto? produtoCadastrado = null;

        repositorioProduto.Setup(r => r.Cadastrar(It.IsAny<Produto>())).Callback<Produto>(produto => produtoCadastrado = produto);

        ServicoProduto servicoProduto = new ServicoProduto(
            repositorioProduto.Object
        );

        //Act
        Result resultado = servicoProduto
            .Cadastrar(new CadastrarProdutoDto("testar", 10));


        //Assert
        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(servicoProduto);

        repositorioProduto.Verify(r => r.Cadastrar(It.IsAny<Produto>()), Times.Once);
    }

    [TestMethod]
    public void Cadastrar_DadosInvalidos_RetornaErro()
    {
        // Arrange
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Act
        Result resultado = servicoProduto
            .Cadastrar(new CadastrarProdutoDto(string.Empty, 10));

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "O Campo Nome precisa ser preenchido;",
            resultado.Errors.First().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_PrecoProdutoCadastrado_NovoPrecoESalvoCorretamente()
    {
        // Arrange
        Produto produto = new("Produto Teste", 10);

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarPorId(produto.Id))
            .Returns(produto);

        repositorioProduto
            .Setup(r => r.Editar(produto.Id, It.IsAny<Produto>()))
            .Returns(true);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Act
        Result resultado = servicoProduto.Editar(
            new EditarProdutoDto(produto.Id, "Produto Teste", 20)
        );

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioProduto.Verify(
            r => r.Editar(
                produto.Id,
                It.Is<Produto>(p => p.Preco == 20)
            ),
            Times.Once
        );
    }

    [TestMethod]
    public void SelecionarTodos_DeveRetornarTodosOsProdutos()
    {
        // Arrange
        Mock<IRepositorioProduto> repositorioProduto = new();

        Produto produto1 = new(
            "Coca-Cola",
            10
        );

        Produto produto2 = new(
            "Dreher",
            30
        );

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([produto1, produto2]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Act
        List<ListarProdutoDto> resultado = servicoProduto.SelecionarTodos();

        // Assert
        resultado.Should().HaveCount(2);

        resultado.Should().BeEquivalentTo(
            new ListarProdutoDto(
                produto1.Id,
                produto1.Nome,
                produto1.Preco
            ),
            new ListarProdutoDto(
                produto2.Id,
                produto2.Nome,
                produto2.Preco
            )
        );

        repositorioProduto.Verify(
            r => r.SelecionarTodos(),
            Times.Once
        );
    }

    [TestMethod]
    public void Excluir_ProdutoCadastrado_DeveExcluirComSucesso()
    {
        // Arrange
        Produto produto = new(
            "Coca-Cola",
            10
        );

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarPorId(produto.Id))
            .Returns(produto);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Act
        Result resultado = servicoProduto.Excluir(produto.Id);

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioProduto.Verify(
            r => r.Excluir(It.IsAny<Guid>()),
            Times.Once
        );
    }


}