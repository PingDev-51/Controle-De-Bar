using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Infra.Compartilhado.Orm;
using GeradorDeProvas.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Infra.Modulos.ModuloPedido;

public sealed class RepositorioPedidoEmOrm(
    ControleDeBarDbContext dbContext
) : RepositorioBaseEmOrm<Pedido>(dbContext), IRepositorioPedido
{
    public override Pedido? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(p => p.Conta)
            .Include(p => p.Produto)
            .SingleOrDefault(p => p.Id == idSelecionado);
    }

    public override List<Pedido> SelecionarTodos()
    {
        return registros
            .Include(p => p.Conta)
            .Include(p => p.Produto)
            .ToList();
    }
}