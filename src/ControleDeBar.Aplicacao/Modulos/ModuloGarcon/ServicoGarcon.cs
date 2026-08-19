using System;
using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloGarcon;

public class ServicoGarcon(
    IRepositorioGarcon repositorioGarcon,
    IRepositorioContas repositorioContas
) : ServicoBase<Garcon>
{
    public Result Cadastrar(CadastrarGarconDto dto)
    {

        Garcon novoGarcon = new(dto.Nome);

        Result resultadoValidacao = ValidarEntidade(novoGarcon);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioGarcon.Cadastrar(novoGarcon);

        return Result.Ok();
    }

    public Result Editar(EditarGarconDto dto)
    {
        Garcon garconAtualizado = new(dto.Nome);

        Result resultadoValidacao = ValidarEntidade(garconAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioGarcon.Editar(dto.Id, garconAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Garçon não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Garcon? garcon = repositorioGarcon.SelecionarPorId(id);

        if (garcon == null)
            return Falha(string.Empty, "Garçom não encontrado.");

        bool garconEmUso = repositorioContas
            .SelecionarTodos()
            .Any(c => c.Garcon != null && c.Garcon.Id == id);

        if (garconEmUso)
            return Falha(
                string.Empty,
                "Não é possível excluir este garçom, pois ele está vinculado a uma conta."
            );

        repositorioGarcon.Excluir(id);

        return Result.Ok();
    }

    public List<ListarGarconDto> SelecionarTodos()
    {
        return repositorioGarcon
            .SelecionarTodos()
            .Select(g => new ListarGarconDto(g.Id, g.Nome))
            .ToList();
    }

    public Result<DetalhesGarconDto> SelecionarPorId(Guid id)
    {
        Garcon? garcon = repositorioGarcon.SelecionarPorId(id);

        if (garcon == null)
            return Result.Fail("Produto não encontrada.");

        return Result.Ok(new DetalhesGarconDto(garcon.Id, garcon.Nome));
    }
}
