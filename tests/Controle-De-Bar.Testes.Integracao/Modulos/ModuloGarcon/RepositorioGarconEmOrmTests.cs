using System;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;
using FizzWare.NBuilder;

namespace Controle_De_Bar.Testes.Integracao.Modulos.ModuloGarcon;

[TestClass]
public class RepositorioGarconEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaRegistro()
    {
        // Arrange
        Garcon garcon = Builder<Garcon>
            .CreateNew()
            .With(g => g.UserId = userId)
            .With(g => g.Nome = "Osvaldo")
            .Build();

        // Act
        repositorioGarcon.Cadastrar(garcon);

        dbContext.ChangeTracker.Clear();

        Garcon? garconSelecionado =
            repositorioGarcon.SelecionarPorId(garcon.Id);

        // Assert
        Assert.IsNotNull(garconSelecionado);
        Assert.AreEqual(garcon.Id, garconSelecionado.Id);
        Assert.AreEqual("Osvaldo", garconSelecionado.Nome);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_ComDadosValidos()
    {
        // Arrange
        Garcon garcon = Builder<Garcon>
            .CreateNew()
            .With(g => g.UserId = userId)
            .With(g => g.Nome = "Osvaldo")
            .Build();

        // Act
        repositorioGarcon.Cadastrar(garcon);

        dbContext.ChangeTracker.Clear();

        Garcon? garconSelecionado =
            repositorioGarcon.SelecionarPorId(garcon.Id);

        // Assert
        Assert.IsNotNull(garconSelecionado);
        Assert.AreEqual(garcon.Id, garconSelecionado.Id);
        Assert.AreEqual("Osvaldo", garconSelecionado.Nome);
    }

    [TestMethod]
    public void Editar_AtualizaRegistroExistente()
    {
        // Arrange
        Garcon garcon = Builder<Garcon>
            .CreateNew()
            .With(g => g.UserId = userId)
            .With(g => g.Nome = "Osvaldo")
            .Persist();

        Garcon garconAtualizado = Builder<Garcon>
            .CreateNew()
            .With(g => g.Nome = "Geraldo")
            .Build();

        // Act
        bool conseguiuEditar =
            repositorioGarcon.Editar(garcon.Id, garconAtualizado);

        dbContext.ChangeTracker.Clear();

        Garcon? garconSelecionado =
            repositorioGarcon.SelecionarPorId(garcon.Id);

        // Assert
        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(garconSelecionado);
        Assert.AreEqual("Geraldo", garconSelecionado.Nome);
    }

    [TestMethod]
    public void Excluir_RemoveRegistroExistente()
    {
        // Arrange
        Garcon garcon = Builder<Garcon>
            .CreateNew()
            .With(g => g.UserId = userId)
            .With(g => g.Nome = "Osvaldo")
            .Persist();

        // Act
        bool conseguiuExcluir =
            repositorioGarcon.Excluir(garcon.Id);

        dbContext.ChangeTracker.Clear();

        Garcon? garconSelecionado =
            repositorioGarcon.SelecionarPorId(garcon.Id);

        // Assert
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(garconSelecionado);
    }

    [TestMethod]
    public void SelecionarTodos_CarregaRegistros()
    {
        // Arrange / Act
        IList<Garcon> garcons = Builder<Garcon>
            .CreateListOfSize(3)
            .All()
            .With(g => g.UserId = userId)
            .Persist();

        dbContext.ChangeTracker.Clear();

        // Assert
        Assert.HasCount(
            3,
            repositorioGarcon.SelecionarTodos()
        );
    }
}
