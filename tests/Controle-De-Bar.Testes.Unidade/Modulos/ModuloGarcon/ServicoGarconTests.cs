using System;
using ControleDeBar.Aplicacao.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using FluentAssertions;
using FluentResults;
using Moq;

namespace Controle_De_Bar.Testes.Unidade.Modulos.ModuloGarcon;

[TestClass]
public class ServicoGarconTests
{
    [TestMethod]
    public void Cadastrar_DadosValidos_PersisteGarcon()
    {
        //Arange
        Garcon garcon = new("testar");

        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioGarcon.Setup(r => r.SelecionarTodos()).Returns([]);

        Garcon? garconCadastrado = null;

        repositorioGarcon.Setup(r => r.Cadastrar(It.IsAny<Garcon>())).Callback<Garcon>(garcon => garconCadastrado = garcon);

        ServicoGarcon servicoGarcon = new ServicoGarcon(
            repositorioGarcon.Object,
            repositorioConta.Object
        );

        //Act
        Result resultado = servicoGarcon
            .Cadastrar(new CadastrarGarconDto("testar"));


        //Assert
        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(servicoGarcon);

        repositorioGarcon.Verify(r => r.Cadastrar(It.IsAny<Garcon>()), Times.Once);
    }

    [TestMethod]
    public void Cadastrar_DadosInvalidos_RetornaErro()
    {
        // Arrange
        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioGarcon
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoGarcon servicoGarcon = new(
            repositorioGarcon.Object,
            repositorioConta.Object
        );

        // Act
        Result resultado = servicoGarcon
            .Cadastrar(new CadastrarGarconDto(string.Empty));

        // Assert
        Assert.IsTrue(resultado.IsFailed);
        Assert.AreEqual(
            "O Campo Nome precisa ser preenchido;",
            resultado.Errors.First().Message
        );

        repositorioGarcon.Verify(
            r => r.Cadastrar(It.IsAny<Garcon>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_GarconCadastrado_SalvoCorretamente()
    {
        // Arrange
        Garcon garcon = new("Teste");

        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioGarcon
            .Setup(r => r.SelecionarPorId(garcon.Id))
            .Returns(garcon);

        repositorioGarcon
            .Setup(r => r.Editar(garcon.Id, It.IsAny<Garcon>()))
            .Returns(true);

        ServicoGarcon servicoGarcon = new(
          repositorioGarcon.Object,
          repositorioConta.Object
        );

        // Act
        Result resultado = servicoGarcon.Editar(
            new EditarGarconDto(garcon.Id, "Teste")
        );

        // Assert
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public void SelecionarTodos_DeveRetornarTodosOsGarcons()
    {
        // Arrange
        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioContas> repositorioConta = new();

        Garcon garcon1 = new(
            "Geraldo"
        );

        Garcon garcon2 = new(
            "Osvaldo"
        );


        repositorioGarcon
            .Setup(r => r.SelecionarTodos())
            .Returns([garcon1, garcon2]);

        ServicoGarcon servicoProduto = new(
            repositorioGarcon.Object,
            repositorioConta.Object
        );

        // Act
        List<ListarGarconDto> resultado = servicoProduto.SelecionarTodos();

        // Assert
        resultado.Should().HaveCount(2);

        resultado.Should().BeEquivalentTo(
            new ListarGarconDto(
                garcon1.Id,
                garcon1.Nome
            ),
            new ListarGarconDto(
                garcon2.Id,
                garcon2.Nome
            )
        );

        repositorioGarcon.Verify(
            r => r.SelecionarTodos(),
            Times.Once
        );
    }

    [TestMethod]
    public void Excluir_ProdutoCadastrado_DeveExcluirComSucesso()
    {
        // Arrange
        Garcon garcon = new(
            "Osvaldo"
        );

        Mock<IRepositorioGarcon> repositorioGarcon = new();
        Mock<IRepositorioContas> repositorioConta = new();

        repositorioGarcon
        .Setup(r => r.SelecionarPorId(garcon.Id))
        .Returns(garcon);

        repositorioConta
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        repositorioGarcon
            .Setup(r => r.SelecionarPorId(garcon.Id))
            .Returns(garcon);

        ServicoGarcon servicoProduto = new(
            repositorioGarcon.Object,
            repositorioConta.Object
        );

        // Act
        Result resultado = servicoProduto.Excluir(garcon.Id);

        // Assert
        Assert.IsTrue(resultado.IsSuccess);

        repositorioGarcon.Verify(
            r => r.Excluir(It.IsAny<Guid>()),
            Times.Once
        );
    }
}
