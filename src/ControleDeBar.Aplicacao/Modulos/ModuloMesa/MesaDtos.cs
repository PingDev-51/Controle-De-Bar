using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Aplicacao.Modulos.ModuloMesa;

public record ListarMesaDto(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    StatusDaMesa StatusDaMesa
);
public record CadastrarMesaDto(
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    int StatusDaMesa
);

