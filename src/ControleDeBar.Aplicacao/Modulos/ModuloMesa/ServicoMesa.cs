using System;
using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloMesa;

public class ServicoMesa(IRepositorioMesa repositorioMesa) : ServicoBase<Mesa>
{
    public Result Cadastrar(CadastrarMesaDto dto)
    {
        StatusDaMesa tipoDeStatus = 0;

        if (dto.StatusDaMesa == 1)
            tipoDeStatus = StatusDaMesa.Livre;
        else if (dto.StatusDaMesa == 2)
            tipoDeStatus = StatusDaMesa.Ocupado;

        Mesa novaMesa = new(dto.NumeroDaMesa, dto.QuantidadeDeLugares, tipoDeStatus);

        Result resultadoValidacao = ValidarEntidade(novaMesa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMesa.Cadastrar(novaMesa);

        return Result.Ok();
    }

    public Result Editar()
    {

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {


        return Result.Ok();
    }

    public List<ListarMesaDto> SelecionarTodos()
    {
        return repositorioMesa
            .SelecionarTodos()
            .Select(m => new ListarMesaDto(m.Id, m.NumeroDaMesa, m.QuantidadeDeLugares, m.StatusDaMesa))
            .ToList();
    }

    // public Result SelecionarPorId(Guid id)
    // {

    // }
}
