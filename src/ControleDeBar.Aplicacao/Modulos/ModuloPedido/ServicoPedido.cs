using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloPedido;
using ControleDeBar.Dominio.Modulos.ModuloProduto;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloPedido;

public class ServicoPedido : ServicoBase<Pedido>
{
    private readonly IRepositorioPedido repositorioPedidos;
    private readonly IRepositorioContas repositorioContas;
    private readonly IRepositorioProduto repositorioProdutos;

    public ServicoPedido(
        IRepositorioPedido repositorioPedidos,
        IRepositorioContas repositorioContas,
        IRepositorioProduto repositorioProdutos)
    {
        this.repositorioPedidos = repositorioPedidos;
        this.repositorioContas = repositorioContas;
        this.repositorioProdutos = repositorioProdutos;
    }

    public Result Cadastrar(CadastrarPedidoDto dto)
    {
        Conta? contaSelecionada =
            repositorioContas.SelecionarPorId(dto.ContaId);

        if (contaSelecionada == null)
            return Falha(string.Empty, "Conta não encontrada.");

        Produto? produtoSelecionado =
            repositorioProdutos.SelecionarPorId(dto.ProdutoId);

        if (produtoSelecionado == null)
            return Falha(string.Empty, "Produto não encontrado.");

        Pedido novoPedido = new Pedido(
            dto.ContaId,
            dto.ProdutoId,
            dto.Quantidade)
        {
            Conta = contaSelecionada,
            Produto = produtoSelecionado
        };

        novoPedido.CalcularTotal();

        Result resultadoValidacao = ValidarEntidade(novoPedido);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioPedidos.Cadastrar(novoPedido);

        return Result.Ok();
    }

    public Result Editar(EditarPedidoDto dto)
    {
        Conta? contaSelecionada =
            repositorioContas.SelecionarPorId(dto.ContaId);

        if (contaSelecionada == null)
            return Falha(string.Empty, "Conta não encontrada.");

        Produto? produtoSelecionado =
            repositorioProdutos.SelecionarPorId(dto.ProdutoId);

        if (produtoSelecionado == null)
            return Falha(string.Empty, "Produto não encontrado.");

        Pedido pedidoAtualizado = new Pedido(
            dto.ContaId,
            dto.ProdutoId,
            dto.Quantidade)
        {
            Conta = contaSelecionada,
            Produto = produtoSelecionado
        };

        pedidoAtualizado.CalcularTotal();

        Result resultadoValidacao = ValidarEntidade(pedidoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar =
            repositorioPedidos.Editar(dto.Id, pedidoAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Pedido não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Pedido? pedido =
            repositorioPedidos.SelecionarPorId(id);

        if (pedido == null)
            return Falha(string.Empty, "Pedido não encontrado.");

        repositorioPedidos.Excluir(id);

        return Result.Ok();
    }

    public List<ListarPedidoDto> SelecionarPorConta(Guid contaId)
    {
        return repositorioPedidos
            .SelecionarPorConta(contaId)
            .Select(p => new ListarPedidoDto(
                p.Id,
                p.ContaId,
                p.ProdutoId,
                p.Produto?.Nome ?? "Produto não encontrado",
                p.Quantidade,
                p.Total
            ))
            .ToList();
    }

    public Result<DetalhesPedidoDto> SelecionarPorId(Guid id)
    {
        Pedido? pedido =
            repositorioPedidos.SelecionarPorId(id);

        if (pedido == null)
            return Result.Fail("Pedido não encontrado.");

        if (pedido.Conta == null)
            return Result.Fail("A conta do pedido não foi encontrada.");

        if (pedido.Produto == null)
            return Result.Fail("O produto do pedido não foi encontrado.");

        return Result.Ok(new DetalhesPedidoDto(
            pedido.Id,
            pedido.ContaId,
            pedido.ProdutoId,
            pedido.Produto.Nome,
            pedido.Quantidade,
            pedido.Total
        ));
    }


    public List<OpcaoProdutoDto> SelecionarProdutos()
    {
        return repositorioProdutos
            .SelecionarTodos()
            .Select(p => new OpcaoProdutoDto(
                p.Id,
                p.Nome,
                p.Preco
            ))
            .ToList();
    }
}