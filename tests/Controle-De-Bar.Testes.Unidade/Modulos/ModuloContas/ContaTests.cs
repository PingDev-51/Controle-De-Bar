using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloContas;

[TestClass]
public sealed class ContaTests()
{
    [TestMethod]
    public void AbrirConta_ComClienteGarconMesaEDataValidos()
    {
        // Arrange
        Garcon garcon = new Garcon("João");

        Mesa mesa = new Mesa(
            "1",
            "2",
            StatusDaMesa.Livre
        );

        Conta conta = new Conta("Kauan");

        conta.Garcon = garcon;
        conta.Mesa = mesa;

        // Act
        List<string> erros = conta.Validar();

        // Assert
        Assert.HasCount(0, erros);
        Assert.AreEqual("Kauan", conta.NomeCliente);
        Assert.AreEqual(Situacao.Aberta, conta.Situacao);
        Assert.AreEqual(garcon, conta.Garcon);
        Assert.AreEqual(mesa, conta.Mesa);
    }

    [TestMethod]
    public void AbrirConta_SemNomeDoCliente()
    {
        // Arrange
        Garcon garcon = new Garcon("João");

        Mesa mesa = new Mesa(
            "1",
            "2",
            StatusDaMesa.Livre
        );

        Conta conta = new Conta(string.Empty);

        conta.Garcon = garcon;
        conta.Mesa = mesa;

        // Act
        List<string> erros = conta.Validar();

        // Assert
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo NomeCliente precisa ser preenchido.",
            erros.First()
        );
    }

    [TestMethod]
    public void ConsultarContaAberta_ComDadosValidos()
    {
        // Arrange
        Garcon garcon = new Garcon("João");

        Mesa mesa = new Mesa(
            "1",
            "2",
            StatusDaMesa.Livre
        );

        Conta conta = new Conta("Kauan");

        conta.Garcon = garcon;
        conta.Mesa = mesa;

        // Act
        List<string> erros = conta.Validar();

        // Assert
        Assert.HasCount(0, erros);
        Assert.AreEqual("Kauan", conta.NomeCliente);
        Assert.AreEqual(Situacao.Aberta, conta.Situacao);
        Assert.AreEqual(garcon, conta.Garcon);
        Assert.AreEqual(mesa, conta.Mesa);
    }
}