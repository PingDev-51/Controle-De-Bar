using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Infra.Compartilhado.Orm;
using GeradorDeProvas.Infra.Compartilhado.Orm;

namespace ControleDeBar.Infra.Modulos.ModuloContas;

public sealed class RepositorioContaEmOrm(
    ControleDeBarDbContext dbContext
) : RepositorioBaseEmOrm<Conta>(dbContext), IRepositorioContas;