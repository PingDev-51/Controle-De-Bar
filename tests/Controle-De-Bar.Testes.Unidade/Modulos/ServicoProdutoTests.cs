using ControleDeBar.Aplicacao.Modulos.ModuloProduto;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
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

        repositorioProduto.Setup(r => r.Cadastrar(It.IsAny<Produto>())).Callback<Produto>(materia => produtoCadastrado = materia);

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
}