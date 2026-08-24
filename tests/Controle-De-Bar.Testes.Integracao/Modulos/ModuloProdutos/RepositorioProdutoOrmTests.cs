using ControleDeBar.Dominio.Modulos.ModuloProduto;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;
using FizzWare.NBuilder;

namespace ControleDeBar.Testes.Integracao.Modulos.ModuloProduto;

[TestClass]
public sealed class RepositorioProdutoEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaRegistro()
    {
        // Arranjo
        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "teste")
            .With(p => p.Preco = 25.90m)
            .Build();

        // Ação
        repositorioProduto.Cadastrar(produto);
        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado =
            repositorioProduto.SelecionarPorId(produto.Id);

        // Asserção
        Assert.IsNotNull(produtoSelecionado);
        Assert.AreEqual(produto.Id, produtoSelecionado.Id);
        Assert.AreEqual("teste", produtoSelecionado.Nome);
        Assert.AreEqual(25.90m, produtoSelecionado.Preco);
    }


    [TestMethod]
    public void CadastrarESelecionarPorId_ComNomeValido()
    {
        // Arranjo
        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "Coca-Cola")
            .With(p => p.Preco = 10)
            .Build();

        // Ação
        repositorioProduto.Cadastrar(produto);

        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado =
            repositorioProduto.SelecionarPorId(produto.Id);

        // Asserção
        Assert.IsNotNull(produtoSelecionado);
        Assert.AreEqual(produto.Id, produtoSelecionado.Id);
        Assert.AreEqual("Coca-Cola", produtoSelecionado.Nome);
        Assert.AreEqual(10, produtoSelecionado.Preco);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_ComPrecoValido()
    {
        // Arranjo
        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "teste")
            .With(p => p.Preco = 18.75m)
            .Build();

        // Ação
        repositorioProduto.Cadastrar(produto);
        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado =
            repositorioProduto.SelecionarPorId(produto.Id);

        // Asserção
        Assert.IsNotNull(produtoSelecionado);
        Assert.AreEqual(produto.Id, produtoSelecionado.Id);
        Assert.AreEqual(18.75m, produtoSelecionado.Preco);
    }

    [TestMethod]
    public void Editar_AtualizaRegistroExistente()
    {
        // Arranjo
        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "teste")
            .With(p => p.Preco = 20.00m)
            .Persist();

        Produto produtoAtualizado = Builder<Produto>
            .CreateNew()
            .With(p => p.Nome = "teste2")
            .With(p => p.Preco = 28.50m)
            .Build();

        // Ação
        bool conseguiuEditar =
            repositorioProduto.Editar(produto.Id, produtoAtualizado);

        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado =
            repositorioProduto.SelecionarPorId(produto.Id);

        // Asserção
        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(produtoSelecionado);
        Assert.AreEqual("teste2", produtoSelecionado.Nome);
        Assert.AreEqual(28.50m, produtoSelecionado.Preco);
    }


    [TestMethod]
    public void Excluir_RemoveRegistroExistente()
    {
        // Arranjo
        Produto produto = Builder<Produto>
            .CreateNew()
            .With(p => p.UserId = userId)
            .With(p => p.Nome = "teste2")
            .With(p => p.Preco = 7.00m)
            .Persist();

        // Ação
        bool conseguiuExcluir =
            repositorioProduto.Excluir(produto.Id);

        dbContext.ChangeTracker.Clear();

        Produto? produtoSelecionado =
            repositorioProduto.SelecionarPorId(produto.Id);

        // Asserção
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(produtoSelecionado);
    }

    [TestMethod]
    public void SelecionarTodos_CarregaRegistros()
    {
        // Arranjo / Ação
        IList<Produto> produtos = Builder<Produto>
            .CreateListOfSize(3)
            .All()
            .With(p => p.UserId = userId)
            .Persist();

        dbContext.ChangeTracker.Clear();

        // Asserção
        Assert.HasCount(
            3,
            repositorioProduto.SelecionarTodos()
        );
    }
}
