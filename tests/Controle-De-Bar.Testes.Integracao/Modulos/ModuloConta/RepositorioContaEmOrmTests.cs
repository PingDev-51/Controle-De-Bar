using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;
using FizzWare.NBuilder;

namespace ControleDeBar.Testes.Integracao.Modulos.ModuloConta;

[TestClass]
public sealed class RepositorioContaEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaRegistro()
    {
        // Arranjo
        Garcon garcon = Builder<Garcon>
            .CreateNew()
            .With(g => g.UserId = userId)
            .With(g => g.Nome = "João")
            .Build();

        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "10")
            .With(m => m.QuantidadeDeLugares = "4")
            .Build();

        dbContext.Garcon.Add(garcon);
        dbContext.Mesa.Add(mesa);
        dbContext.SaveChanges();

        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Kauan")
            .With(c => c.Garcon = garcon)
            .With(c => c.Mesa = mesa)
            .With(c => c.DataAbertura = DateTime.Now)
            .With(c => c.Situacao = Situacao.Aberta)
            .Build();

        // Ação
        repositorioConta.Cadastrar(conta);

        dbContext.ChangeTracker.Clear();

        Conta? contaSelecionada =
            repositorioConta.SelecionarPorId(conta.Id);

        // Asserção
        Assert.IsNotNull(contaSelecionada);

        Assert.AreEqual(
            conta.Id,
            contaSelecionada.Id
        );

        Assert.AreEqual(
            "Kauan",
            contaSelecionada.NomeCliente
        );

        Assert.IsNotNull(contaSelecionada.Garcon);

        Assert.AreEqual(
            garcon.Id,
            contaSelecionada.Garcon.Id
        );

        Assert.AreEqual(
            "João",
            contaSelecionada.Garcon.Nome
        );

        Assert.IsNotNull(contaSelecionada.Mesa);

        Assert.AreEqual(
            mesa.Id,
            contaSelecionada.Mesa.Id
        );

        Assert.AreEqual(
            "10",
            contaSelecionada.Mesa.NumeroDaMesa
        );

        Assert.AreEqual(
            Situacao.Aberta,
            contaSelecionada.Situacao
        );
    }



    [TestMethod]
    public void SelecionarPorId_ContaAberta_CarregaDadosRelacionados()
    {
        // Arranjo
        Garcon garcon = Builder<Garcon>
            .CreateNew()
            .With(g => g.UserId = userId)
            .With(g => g.Nome = "Carlos")
            .Build();

        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.UserId = userId)
            .With(m => m.NumeroDaMesa = "15")
            .With(m => m.QuantidadeDeLugares = "6")
            .Build();

        dbContext.Garcon.Add(garcon);
        dbContext.Mesa.Add(mesa);
        dbContext.SaveChanges();

        Conta conta = Builder<Conta>
            .CreateNew()
            .With(c => c.UserId = userId)
            .With(c => c.NomeCliente = "Maria")
            .With(c => c.Garcon = garcon)
            .With(c => c.Mesa = mesa)
            .With(c => c.DataAbertura = DateTime.Now)
            .With(c => c.Situacao = Situacao.Aberta)
            .Persist();

        dbContext.ChangeTracker.Clear();

        // Ação
        Conta? contaSelecionada =
            repositorioConta.SelecionarPorId(conta.Id);

        // Asserção
        Assert.IsNotNull(contaSelecionada);

        Assert.AreEqual(
            "Maria",
            contaSelecionada.NomeCliente
        );

        Assert.AreEqual(
            Situacao.Aberta,
            contaSelecionada.Situacao
        );

        Assert.IsNotNull(contaSelecionada.Garcon);

        Assert.AreEqual(
            "Carlos",
            contaSelecionada.Garcon.Nome
        );

        Assert.IsNotNull(contaSelecionada.Mesa);

        Assert.AreEqual(
            "15",
            contaSelecionada.Mesa.NumeroDaMesa
        );
    }
}