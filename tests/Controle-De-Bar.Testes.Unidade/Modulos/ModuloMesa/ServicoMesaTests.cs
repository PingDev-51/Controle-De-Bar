using System;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentAssertions;
using FluentResults;
using Moq;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloMesa;

[TestClass]
public class ServicoMesaTests
{
    [TestMethod]
    public void Cadastrar_DadosValidos_PersisteMesa()
    {
        //Arange
        Mesa produto = new("testar", "Testar", StatusDaMesa.Indefiniodo);

        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioMesa.Setup(r => r.SelecionarTodos()).Returns([]);

        Mesa? mesaCadastrado = null;

        repositorioMesa.Setup(r => r.Cadastrar(It.IsAny<Mesa>())).Callback<Mesa>(mesa => mesaCadastrado = mesa);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        //Act
        Result resultado = servicoMesa
            .Cadastrar(new CadastrarMesaDto("testar", "testar", 0));


        //Assert
        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(servicoMesa);

        repositorioMesa.Verify(r => r.Cadastrar(It.IsAny<Mesa>()), Times.Once);
    }

    [TestMethod]
    public void Cadastrar_DadosInvalidos_Do_NumeroDaMesa_RetornaErro()
    {
        // Arrange
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        // Act
        Result resultado = servicoMesa
            .Cadastrar(new CadastrarMesaDto(string.Empty, QuantidadeDeLugares: "2", 0));

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "O Campo Numero Da Mesa precisa ser preenchido;",
            resultado.Errors.First().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_DadosInvalidos_Do_QuantidadeDeLugares_RetornaErro()
    {
        // Arrange
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        // Act
        Result resultado = servicoMesa
            .Cadastrar(new CadastrarMesaDto(NumeroDaMesa: "2", string.Empty, 0));

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "O Campo Quantidade De Lugares precisa ser preenchido;",
            resultado.Errors.First().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_MesaCadastrada_SalvoCorretamente()
    {
        // Arrange
        Mesa mesa = new("Testar", "Testar", 0);

        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);

        repositorioMesa
            .Setup(r => r.Editar(mesa.Id, It.IsAny<Mesa>()))
            .Returns(true);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        // Act
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(mesa.Id, "Teste1", "Teste2", 0)
        );

        // Assert
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public void SelecionarTodos_DeveRetornarTodos_Os_DadosDaMesa()
    {
        // Arrange
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioContas> repositorioConta = new();

        Mesa mesa1 = new(
            numeroDaMesa: "1",
            quantidadeDeLugares: "3",
            statusDaMesa: StatusDaMesa.Livre
        );

        Mesa mesa2 = new(
            numeroDaMesa: "1",
            quantidadeDeLugares: "3",
            statusDaMesa: StatusDaMesa.Livre
        );

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesa1, mesa2]);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        // Act
        List<ListarMesaDto> resultado = servicoMesa.SelecionarTodos();

        // Assert
        resultado.Should().HaveCount(2);

        resultado.Should().BeEquivalentTo(
            new ListarMesaDto(
                mesa1.Id,
                mesa1.NumeroDaMesa,
                mesa1.QuantidadeDeLugares,
                mesa1.StatusDaMesa
            ),
            new ListarMesaDto(
                mesa2.Id,
                mesa2.NumeroDaMesa,
                mesa2.QuantidadeDeLugares,
                mesa2.StatusDaMesa
            )
        );

        repositorioMesa.Verify(
            r => r.SelecionarTodos(),
            Times.Once
        );
    }

    [TestMethod]

    public void Excluir_MesaCadastrado_DeveExcluirComSucesso()
    {
        // Arrange
        Mesa mesa = new(
            "1",
            "3",
            StatusDaMesa.Livre
        );

        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioConta
            .Setup(r => r.SelecionarTodos())
            .Returns(new List<Conta>());

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object,
            repositorioConta.Object
        );

        // Act
        Result resultado = servicoMesa.Excluir(mesa.Id);

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioMesa.Verify(
            r => r.Excluir(It.IsAny<Guid>()),
            Times.Once
        );
    }
}
