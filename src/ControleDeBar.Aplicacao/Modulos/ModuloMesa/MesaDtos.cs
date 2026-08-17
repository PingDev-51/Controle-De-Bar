using System;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Aplicacao.Modulos.ModuloMesa;

public record ListarMesaDto(
    Guid Id,
    string NumeroDaMesa,
    string QuantidadeDeLugares,
    string Senha,
    StatusDaMesa StatusDaMesa
);
