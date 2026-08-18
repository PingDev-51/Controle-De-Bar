using System;

namespace ControleDeBar.Aplicacao.Modulos.ModuloPedido;

public record ListarPedidoDto(
    Guid Id,
    Guid ContaId,
    Guid ProdutoId,
    string NomeProduto,
    int Quantidade,
    decimal Total
);

public record CadastrarPedidoDto(
    Guid ContaId,
    Guid ProdutoId,
    int Quantidade
);

public record EditarPedidoDto(
    Guid Id,
    Guid ContaId,
    Guid ProdutoId,
    int Quantidade
);

public record ExcluirPedidoDto(
    Guid Id,
    Guid ContaId
);

public record DetalhesPedidoDto(
    Guid Id,
    Guid ContaId,
    Guid ProdutoId,
    string NomeProduto,
    int Quantidade,
    decimal Total
);

public record OpcaoProdutoDto(
    Guid Id,
    string Nome,
    decimal Preco
);