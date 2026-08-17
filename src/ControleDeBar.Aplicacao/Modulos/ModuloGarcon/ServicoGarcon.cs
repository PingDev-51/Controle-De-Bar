using System;
using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloGarcon;

public class ServicoGarcon(IRepositorioGarcon repositorioGarcon) : ServicoBase<Garcon>
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

    // public Result Editar(EditarMesaDto dto)
    // {
    //     StatusDaMesa tipoDeStatus = 0;

    //     if (dto.StatusDaMesa == 1)
    //         tipoDeStatus = StatusDaMesa.Livre;
    //     else if (dto.StatusDaMesa == 2)
    //         tipoDeStatus = StatusDaMesa.Ocupado;

    //     Mesa mesaAtualizada = new(dto.NumeroDaMesa, dto.QuantidadeDeLugares, tipoDeStatus);

    //     Result resultadoValidacao = ValidarEntidade(mesaAtualizada);

    //     if (resultadoValidacao.IsFailed)
    //         return resultadoValidacao;

    //     bool conseguiuEditar = repositorioMesa.Editar(dto.Id, mesaAtualizada);

    //     if (!conseguiuEditar)
    //         return Falha(string.Empty, "Mesa não encontrada.");

    //     return Result.Ok();
    // }

    // public Result Excluir(Guid id)
    // {
    //     Mesa? mesa = repositorioMesa.SelecionarPorId(id);

    //     if (mesa == null)
    //         return Falha(string.Empty, "Mesa não encontrada.");

    //     repositorioMesa.Excluir(id);

    //     return Result.Ok();
    // }

    public List<ListarGarconDto> SelecionarTodos()
    {
        return repositorioGarcon
            .SelecionarTodos()
            .Select(g => new ListarGarconDto(g.Id, g.Nome))
            .ToList();
    }

    // public Result<DetalhesMesaDto> SelecionarPorId(Guid id)
    // {
    //     Mesa? mesa = repositorioMesa.SelecionarPorId(id);

    //     if (mesa == null)
    //         return Result.Fail("Produto não encontrada.");

    //     return Result.Ok(new DetalhesMesaDto(mesa.Id, mesa.NumeroDaMesa, mesa.QuantidadeDeLugares, mesa.StatusDaMesa));
    // }
}
