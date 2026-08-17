using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.WebApp.Modulos.ModuloMesa;

public record ListarMesaViewModel(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    StatusDaMesa StatusDaMesa
);

public record CadastrarMesaViewModel(
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    int StatusDaMesa
);
