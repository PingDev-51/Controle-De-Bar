using System;

namespace ControleDeBar.Aplicacao.Modulos.ModuloGarcon;

public record ListarGarconDto(
    Guid Id,
    string Nome
);
public record CadastrarGarconDto(
    string Nome
);
public record EditarGarconDto(
    Guid Id,
    string Nome
);
public record ExcluirGarconDto(
    Guid Id,
    string Nome
);
public record DetalhesGarconDto(
    Guid Id,
    string Nome
);
