using System;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloGarcon;

[TestClass]
public class GarconTests
{
    [TestMethod]
    public void CadastrarGarcon_ComTodosOsCamposPreenchidos()
    {
        // Arrange
        Garcon garcon = new Garcon(
            "Testar"
        );

        // Act
        List<string> erros = garcon.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }

    [TestMethod]
    public void CadastrarProduto_ComONomeInvalido()
    {
        // Arrange
        Garcon garcon = new Garcon(
            string.Empty
        );

        // Act
        List<string> erros = garcon.Validar();

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
        Garcon garcon = new Garcon(
            "Testar"
        );

        Garcon garconAtualizado = new Garcon("TestarAtualizado");

        // Act
        garcon.Atualizar(garconAtualizado);
        List<string> erros = garcon.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }
}
