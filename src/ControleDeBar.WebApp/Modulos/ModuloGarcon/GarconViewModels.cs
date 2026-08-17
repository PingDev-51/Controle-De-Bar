using System;

namespace ControleDeBar.WebApp.Modulos.ModuloGarcon;

public record ListarGarconViewModel(
    Guid Id,
    string Nome
);

public record CadastrarGarconViewModel(
    string Nome
);

public record EditarGarconViewModel(
    Guid Id,
    string Nome
);

public record ExcluirGarconViewModel(
    Guid Id,
    string Nome
);
