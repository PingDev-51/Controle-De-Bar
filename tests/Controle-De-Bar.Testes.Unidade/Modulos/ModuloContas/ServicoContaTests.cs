using ControleDeBar.Aplicacao.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;
using Moq;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloContas;

[TestClass]
public sealed class ServicoContaTests
{
    [TestMethod]
    public void Cadastrar_DadosValidos_PersisteConta()
    {
        // Arrange
        Guid garconId = Guid.NewGuid();
        Guid mesaId = Guid.NewGuid();

        Garcon garcon = new Garcon
        {
            Id = garconId,
            Nome = "João"
        };

        Mesa mesa = new Mesa
        {
            Id = mesaId,
            NumeroDaMesa = "10"
        };

        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioMesa> repositorioMesa = new();

        repositorioGarcon
            .Setup(r => r.SelecionarPorId(garconId))
            .Returns(garcon);

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesaId))
            .Returns(mesa);

        ServicoConta servicoConta = new(
            repositorioContas.Object,
            repositorioGarcon.Object,
            repositorioMesa.Object
        );

        // Act
        Result resultado = servicoConta.Cadastrar(
            new CadastrarContaDto(
                "Kauan",
                garconId,
                mesaId
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioContas.Verify(
            r => r.Cadastrar(
                It.Is<Conta>(c =>
                    c.NomeCliente == "Kauan" &&
                    c.Garcon == garcon &&
                    c.Mesa == mesa &&
                    c.Situacao == Situacao.Aberta
                )
            ),
            Times.Once
        );
    }


    [TestMethod]
    public void Cadastrar_SemNomeCliente_RetornaErro()
    {
        // Arrange
        Guid garconId = Guid.NewGuid();
        Guid mesaId = Guid.NewGuid();

        Garcon garcon = new Garcon
        {
            Id = garconId,
            Nome = "João"
        };

        Mesa mesa = new Mesa
        {
            Id = mesaId,
            NumeroDaMesa = "10"
        };

        Mock<IRepositorioContas> repositorioContas = new();
        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioMesa> repositorioMesa = new();

        repositorioGarcon
            .Setup(r => r.SelecionarPorId(garconId))
            .Returns(garcon);

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesaId))
            .Returns(mesa);

        ServicoConta servicoConta = new(
            repositorioContas.Object,
            repositorioGarcon.Object,
            repositorioMesa.Object
        );

        // Act
        Result resultado = servicoConta.Cadastrar(
            new CadastrarContaDto(
                string.Empty,
                garconId,
                mesaId
            )
        );

        // Assert
        Assert.IsTrue(resultado.IsFailed);

        Assert.AreEqual(
            "O campo NomeCliente precisa ser preenchido.",
            resultado.Errors.First().Message
        );

        repositorioContas.Verify(
            r => r.Cadastrar(It.IsAny<Conta>()),
            Times.Never
        );
    }


    [TestMethod]
    public void SelecionarPorId_ContaAberta_RetornaDadosCorretamente()
    {
        // Arrange
        Guid garconId = Guid.NewGuid();
        Guid mesaId = Guid.NewGuid();

        Garcon garcon = new Garcon
        {
            Id = garconId,
            Nome = "João"
        };

        Mesa mesa = new Mesa
        {
            Id = mesaId,
            NumeroDaMesa = "10"
        };

        Conta conta = new Conta("Kauan")
        {
            Id = Guid.NewGuid(),
            Garcon = garcon,
            Mesa = mesa,
            Situacao = Situacao.Aberta
        };

        Mock<IRepositorioContas> repositorioContas = new();

        Mock<IRepositorioGarcon> repositorioGarcon = new();

        Mock<IRepositorioMesa> repositorioMesa = new();

        repositorioContas
            .Setup(r => r.SelecionarPorId(conta.Id))
            .Returns(conta);

        ServicoConta servicoConta = new(
            repositorioContas.Object,
            repositorioGarcon.Object,
            repositorioMesa.Object
        );

        // Act
        Result<DetalhesContaDto> resultado =
            servicoConta.SelecionarPorId(conta.Id);

        // Assert
        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(resultado.Value);

        Assert.AreEqual(
            conta.Id,
            resultado.Value.Id
        );

        Assert.AreEqual(
            "Kauan",
            resultado.Value.NomeCliente
        );

        Assert.AreEqual(
            garcon.Id,
            resultado.Value.GarconId
        );

        Assert.AreEqual(
            "João",
            resultado.Value.GarconNome
        );

        Assert.AreEqual(
            mesa.Id,
            resultado.Value.MesaId
        );

        Assert.AreEqual(
            "10",
            resultado.Value.NumeroDaMesa
        );

        Assert.AreEqual(
            Situacao.Aberta,
            resultado.Value.Situacao
        );

        repositorioContas.Verify(
            r => r.SelecionarPorId(conta.Id),
            Times.Once
        );
    }
}