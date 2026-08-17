using System.ComponentModel.DataAnnotations;
using ControleDeBar.Dominio.Modulos.ModuloContas;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

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
    [Required(ErrorMessage = "O campo \"Nome do Cliente\" deve ser preenchido.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "O campo \"Nome do Cliente\" deve conter entre 2 e 100 caracteres."
    )]
    string NomeCliente,

    [Required(ErrorMessage = "O campo \"Garçom\" deve ser selecionado.")]
    Guid GarconId,

    [Required(ErrorMessage = "O campo \"Mesa\" deve ser selecionado.")]
    Guid MesaId
);

public record EditarContaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome do Cliente\" deve ser preenchido.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "O campo \"Nome do Cliente\" deve conter entre 2 e 100 caracteres."
    )]
    string NomeCliente,

    [Required(ErrorMessage = "O campo \"Garçom\" deve ser selecionado.")]
    Guid GarconId,

    [Required(ErrorMessage = "O campo \"Mesa\" deve ser selecionado.")]
    Guid MesaId,

    Situacao Situacao
);

public record ExcluirContaViewModel(
    Guid Id,
    string NomeCliente
);