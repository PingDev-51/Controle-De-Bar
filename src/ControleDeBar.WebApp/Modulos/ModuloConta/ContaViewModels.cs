using ControleDeBar.Dominio.Modulos.ModuloContas;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

public record OpcaoGarconViewModel(
    Guid Id,
    string Nome
);

public record OpcaoMesaViewModel(
    Guid Id,
    string NumeroDaMesa
);

public record ListarContaViewModel(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    string GarconNome,
    Guid MesaId,
    string NumeroDaMesa,
    DateTime DataAbertura,
    Situacao Situacao
);

public record CadastrarContaViewModel(
    string NomeCliente,
    Guid GarconId,
    Guid MesaId
);

public record EditarContaViewModel(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    Guid MesaId,
    Situacao Situacao
);

public record ExcluirContaViewModel(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    Guid MesaId,
    Situacao Situacao
);

public record DetalhesContaViewModel(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    string GarconNome,
    Guid MesaId,
    string NumeroDaMesa,
    DateTime DataAbertura,
    Situacao Situacao
);