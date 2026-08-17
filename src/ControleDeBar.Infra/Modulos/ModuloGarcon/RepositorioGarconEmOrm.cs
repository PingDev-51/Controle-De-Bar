using System;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Infra.Compartilhado.Orm;
using GeradorDeProvas.Infra.Compartilhado.Orm;

namespace ControleDeBar.Infra.Modulos.ModuloGarcon;

public class RepositorioGarconEmOrm(ControleDeBarDbContext dbContext) : RepositorioBaseEmOrm<Garcon>(dbContext), IRepositorioGarcon;
