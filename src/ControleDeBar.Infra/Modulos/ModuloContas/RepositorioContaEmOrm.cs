using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Infra.Compartilhado.Orm;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Infra.Modulos.ModuloContas;

public sealed class RepositorioContaEmOrm(
    ControleDeBarDbContext dbContext
) : RepositorioBaseEmOrm<Conta>(dbContext), IRepositorioContas
{
    public override Conta? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(c => c.Garcon)
            .Include(c => c.Mesa)
        .SingleOrDefault(c => c.Id == idSelecionado);
    }
    public override List<Conta> SelecionarTodos()
    {
        return registros.
            Include(c => c.Garcon)
            .Include(c => c.Mesa)
       .ToList();
    }
}