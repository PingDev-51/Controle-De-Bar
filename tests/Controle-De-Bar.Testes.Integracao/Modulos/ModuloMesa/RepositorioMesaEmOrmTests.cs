using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;
using FizzWare.NBuilder;

namespace Controle_De_Bar.Testes.Integracao.Modulos.ModuloMesa;

[TestClass]
public class RepositorioMesaEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaRegistro()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "teste")
            .With(m => m.QuantidadeDeLugares = "teste")
            .With(m => m.StatusDaMesa = StatusDaMesa.Indefiniodo)
            .Build();

        // Ação
        repositorioMesa.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionado =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsNotNull(mesaSelecionado);
        Assert.AreEqual(mesa.Id, mesaSelecionado.Id);
        Assert.AreEqual("teste", mesaSelecionado.NumeroDaMesa);
        Assert.AreEqual("teste", mesaSelecionado.QuantidadeDeLugares);
        Assert.AreEqual(StatusDaMesa.Indefiniodo, mesaSelecionado.StatusDaMesa);
    }


    [TestMethod]
    public void CadastrarESelecionarPorId_ComNomeValido()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "teste")
            .With(m => m.QuantidadeDeLugares = "teste")
            .With(m => m.StatusDaMesa = StatusDaMesa.Indefiniodo)
            .Build();
        // Ação
        repositorioMesa.Cadastrar(mesa);

        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionado =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsNotNull(mesaSelecionado);
        Assert.AreEqual(mesa.Id, mesaSelecionado.Id);
        Assert.AreEqual("teste", mesaSelecionado.NumeroDaMesa);
        Assert.AreEqual("teste", mesaSelecionado.QuantidadeDeLugares);
        Assert.AreEqual(StatusDaMesa.Indefiniodo, mesaSelecionado.StatusDaMesa);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_ComDadosValidos()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "teste")
            .With(m => m.QuantidadeDeLugares = "teste")
            .With(m => m.StatusDaMesa = StatusDaMesa.Indefiniodo)
            .Build();

        // Ação
        repositorioMesa.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionado =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsNotNull(mesaSelecionado);
        Assert.AreEqual(mesa.Id, mesaSelecionado.Id);
        Assert.AreEqual("teste", mesaSelecionado.NumeroDaMesa);
        Assert.AreEqual("teste", mesaSelecionado.QuantidadeDeLugares);
        Assert.AreEqual(StatusDaMesa.Indefiniodo, mesaSelecionado.StatusDaMesa);
    }

    [TestMethod]
    public void Editar_AtualizaRegistroExistente()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "teste")
            .With(m => m.QuantidadeDeLugares = "teste")
            .With(m => m.StatusDaMesa = StatusDaMesa.Indefiniodo)
            .Persist();

        Mesa mesaAtualizada = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = "teste2")
            .With(m => m.QuantidadeDeLugares = "teste2")
            .With(m => m.StatusDaMesa = StatusDaMesa.Indefiniodo)
            .Build();

        // Ação
        bool conseguiuEditar =
            repositorioMesa.Editar(mesa.Id, mesaAtualizada);

        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionado =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(mesaSelecionado);
        Assert.AreEqual("teste2", mesaSelecionado.NumeroDaMesa);
        Assert.AreEqual("teste2", mesaSelecionado.QuantidadeDeLugares);
        Assert.AreEqual(StatusDaMesa.Indefiniodo, mesaSelecionado.StatusDaMesa);
    }


    [TestMethod]
    public void Excluir_RemoveRegistroExistente()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "teste")
            .With(m => m.QuantidadeDeLugares = "teste")
            .With(m => m.StatusDaMesa = StatusDaMesa.Indefiniodo)
            .Persist();

        // Ação
        bool conseguiuExcluir =
            repositorioMesa.Excluir(mesa.Id);

        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(mesaSelecionada);
    }

    [TestMethod]
    public void SelecionarTodos_CarregaRegistros()
    {
        // Arranjo / Ação
        IList<Mesa> produtos = Builder<Mesa>
            .CreateListOfSize(3)
            .All()
            .With(m => m.UserId = userId)
            .Persist();

        dbContext.ChangeTracker.Clear();

        // Asserção
        Assert.HasCount(
            3,
            repositorioMesa.SelecionarTodos()
        );
    }
}
