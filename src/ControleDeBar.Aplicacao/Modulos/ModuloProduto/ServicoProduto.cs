

using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloProduto;

public class ServicoProduto(
    IRepositorioProduto repositorioProduto
) : ServicoBase<Produto>
{
    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Produto novaProduto = new(dto.Nome, dto.Preco);

        Result resultadoValidacao = ValidarEntidade(novaProduto);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioProduto.Cadastrar(novaProduto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        Produto ProdutoAtualizada = new(dto.Nome, dto.Preco);

        Result resultadoValidacao = ValidarEntidade(ProdutoAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioProduto.Editar(dto.Id, ProdutoAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Produto não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Produto? Produto = repositorioProduto.SelecionarPorId(id);

        if (Produto == null)
            return Falha(string.Empty, "Produto não encontrado.");

        repositorioProduto.Excluir(id);

        return Result.Ok();
    }

    public List<ListarProdutoDto> SelecionarTodos()
    {
        return repositorioProduto
            .SelecionarTodos()
            .Select(d => new ListarProdutoDto(d.Id, d.Nome, d.Preco))
            .ToList();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(Guid id)
    {
        Produto? Produto = repositorioProduto.SelecionarPorId(id);

        if (Produto == null)
            return Result.Fail("Produto não encontrada.");

        return Result.Ok(new DetalhesProdutoDto(Produto.Id, Produto.Nome, Produto.Preco));
    }

}