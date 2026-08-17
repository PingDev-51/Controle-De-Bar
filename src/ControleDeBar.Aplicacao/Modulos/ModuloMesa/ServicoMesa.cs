using System;
using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloMesa;

public class ServicoMesa(IRepositorioMesa repositorioMesa) : ServicoBase<Mesa>
{
    public Result Cadastrar()
    {

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
            .Select(m => new ListarMesaDto(m.Id, m.NumeroDaMesa, m.QuantidadeDeLugares, m.Senha, m.StatusDaMesa))
            .ToList();
    }

    // public Result SelecionarPorId(Guid id)
    // {

    // }
}
