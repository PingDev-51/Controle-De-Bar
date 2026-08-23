using System;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloGarcon;

[TestClass]
public class GarconTests
{
    [TestMethod]
    public void CadastrarProduto_ComTodosOsCamposPreenchidos()
    {
        // Arrange
        Produto produto = new Produto(
            "Testar",
            10
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }

    [TestMethod]
    public void CadastrarProduto_ComONomeInvalido()
    {
        // Arrange
        Produto produto = new Produto(
            string.Empty,
            10
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O Campo Nome precisa ser preenchido;",
            erros.First()
        );
    }

    [TestMethod]
    public void AtualizarProduto()
    {
        // Arrange
        Produto produto = new Produto(
            "Testar",
            10
        );

        Produto produtoAtualizado = new Produto("TestarAtualizado", 20);

        // Act
        produto.Atualizar(produtoAtualizado);
        List<string> erros = produto.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }
}
