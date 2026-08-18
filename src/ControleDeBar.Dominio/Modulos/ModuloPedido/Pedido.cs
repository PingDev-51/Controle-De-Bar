using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.Compartilhado.Identity;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloProduto;

namespace ControleDeBar.Dominio.Modulos.ModuloPedido;

public class Pedido : EntidadeBase<Pedido>, IEntidadeDoUsuario
{
    public Guid UserId { get; set; }

    public Guid ContaId { get; set; }
    public Conta? Conta { get; set; }
    public Guid ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal Total { get; private set; }

    public Pedido() { }

    public Pedido(Guid contaId, Guid produtoId, int quantidade)
    {
        ContaId = contaId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }

    public void CalcularTotal()
    {
        if (Produto is not null)
            Total = Produto.Preco * Quantidade;
    }

    public override void Atualizar(Pedido entidadeAtualizada)
    {
        ContaId = entidadeAtualizada.ContaId;
        ProdutoId = entidadeAtualizada.ProdutoId;
        Quantidade = entidadeAtualizada.Quantidade;
        Total = entidadeAtualizada.Total;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (ContaId == Guid.Empty)
            erros.Add("A Conta precisa ser informada.");

        if (ProdutoId == Guid.Empty)
            erros.Add("O Produto precisa ser informado.");

        if (Quantidade <= 0)
            erros.Add("A Quantidade deve ser maior que zero.");

        return erros;
    }
}