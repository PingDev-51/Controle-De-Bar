using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.WebApp.Modulos.ModuloMesa;

public record ListarMesaViewModel(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    string Senha,
    StatusDaMesa StatusDaMesa
);
