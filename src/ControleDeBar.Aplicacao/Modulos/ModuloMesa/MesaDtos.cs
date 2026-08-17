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

public record EditarMesaDto(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    int StatusDaMesa
);
public record ExcluirMesaDto(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    StatusDaMesa StatusDaMesa
);

public record DetalhesMesaDto(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    StatusDaMesa StatusDaMesa
);

