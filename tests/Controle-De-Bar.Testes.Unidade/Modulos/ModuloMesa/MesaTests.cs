using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloMesa;

[TestClass]
public sealed class MesaTests()
{
    [TestMethod]
    public void CadastrarMesa_ComTodosOsCamposPreenchidos()
    {
        // Arrange
        Mesa mesa = new Mesa(
            "Testar",
            "Testar",
            StatusDaMesa.Indefiniodo
        );

        // Act
        List<string> erros = mesa.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }

    [TestMethod]
    public void CadastrarMesa_ComONumeroDaMesaInvalido()
    {
        // Arrange
        Mesa mesa = new Mesa()
        {
            QuantidadeDeLugares = "1"
        };

        // Act
        List<string> erros = mesa.Validar();

        // Assert
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O Campo Numero Da Mesa precisa ser preenchido;",
            erros.First()
        );
    }

    [TestMethod]
    public void CadastrarMesa_ComOQuantidadeDeLugaresInvalido()
    {
        // Arrange
        Mesa mesa = new Mesa()
        {
            NumeroDaMesa = "1"
        };

        // Act
        List<string> erros = mesa.Validar();

        // Assert
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O Campo Quantidade De Lugares precisa ser preenchido;",
            erros.First()
        );
    }

    [TestMethod]
    public void AtualizarMesa()
    {
        // Arrange
        Mesa mesa = new Mesa(
            "Testar",
            "Testar",
            StatusDaMesa.Indefiniodo
        );

        Mesa mesaAtualizada = new Mesa("TestarAtualizado", "TestarAtualizado", StatusDaMesa.Indefiniodo);

        // Act
        mesa.Atualizar(mesaAtualizada);
        List<string> erros = mesa.Validar();

        // Assert
        Assert.HasCount(0, erros);
    }
}
