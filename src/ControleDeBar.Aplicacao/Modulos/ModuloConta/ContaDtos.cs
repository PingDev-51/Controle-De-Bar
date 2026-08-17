using ControleDeBar.Dominio.Modulos.ModuloContas;

namespace ControleDeBar.Aplicacao.Modulos.ModuloConta;

public record OpcaoGarconDto(
    Guid Id,
    string Nome
);

public record OpcaoMesaDto(
    Guid Id,
    string NumeroDaMesa
);

public record ListarContaDto(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    string GarconNome,
    Guid MesaId,
    string NumeroDaMesa,
    DateTime DataAbertura,
    Situacao Situacao
);

public record CadastrarContaDto(
    string NomeCliente,
    Guid GarconId,
    Guid MesaId
);

public record EditarContaDto(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    Guid MesaId,
    Situacao Situacao
);

public record DetalhesContaDto(
    Guid Id,
    string NomeCliente,
    Guid GarconId,
    string GarconNome,
    Guid MesaId,
    string NumeroDaMesa,
    DateTime DataAbertura,
    Situacao Situacao
);