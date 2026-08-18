using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Modulos.ModuloPedido;

public record ListarPedidoViewModel(
    Guid Id,
    Guid ContaId,
    Guid ProdutoId,
    string NomeProduto,
    int Quantidade,
    decimal Total
);

public record CadastrarPedidoViewModel(
    Guid ContaId,

    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido.")]
    Guid ProdutoId,

    [Required(ErrorMessage = "O campo \"Quantidade\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int Quantidade
);

public record EditarPedidoViewModel(
    Guid Id,
    Guid ContaId,

    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido.")]
    Guid ProdutoId,

    [Required(ErrorMessage = "O campo \"Quantidade\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    int Quantidade
);

public record ExcluirPedidoViewModel(
    Guid Id,
    Guid ContaId,
    string NomeProduto,
    int Quantidade,
    decimal Total
);

public record DetalhesPedidoViewModel(
    Guid Id,
    Guid ContaId,
    Guid ProdutoId,
    string NomeProduto,
    int Quantidade,
    decimal Total
);