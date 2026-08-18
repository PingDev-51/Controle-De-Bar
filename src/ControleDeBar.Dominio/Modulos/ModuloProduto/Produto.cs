using System;
using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.Compartilhado.Identity;

namespace ControleDeBar.Dominio.Modulos.ModuloProduto;

public class Produto : EntidadeBase<Produto>, IEntidadeDoUsuario
{
    public Guid UserId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }


    public Produto() { }

    public Produto(string nome, decimal preco) : this()
    {
        Nome = nome;
        Preco = preco;
    }

    public override void Atualizar(Produto entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Preco = entidadeAtualizada.Preco;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O Campo Nome precisa ser preenchido;");

        return erros;
    }
}
