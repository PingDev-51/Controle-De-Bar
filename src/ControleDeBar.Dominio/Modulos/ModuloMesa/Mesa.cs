using System;
using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.Modulos.ModuloMesa;

public class Mesa : EntidadeBase<Mesa>
{
    public Guid UserId { get; set; }
    public string NumeroDaMesa { get; set; } = string.Empty;
    public string QuantidadeDeLugares { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public StatusDaMesa StatusDaMesa { get; set; }

    public Mesa() { }

    public Mesa(string numeroDaMesa, string quantidadeDeLugares, string senha, StatusDaMesa statusDaMesa)
    {
        NumeroDaMesa = numeroDaMesa;
        QuantidadeDeLugares = quantidadeDeLugares;
        Senha = senha;
        StatusDaMesa = statusDaMesa;
    }

    public override void Atualizar(Mesa entidadeAtualizada)
    {
        NumeroDaMesa = entidadeAtualizada.NumeroDaMesa;
        QuantidadeDeLugares = entidadeAtualizada.QuantidadeDeLugares;
        Senha = entidadeAtualizada.Senha;
        StatusDaMesa = entidadeAtualizada.StatusDaMesa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(NumeroDaMesa))
            erros.Add("O Campo Numero Da Mesa precisa ser preenchido;");

        if (string.IsNullOrWhiteSpace(QuantidadeDeLugares))
            erros.Add("O Campo Quantidade De Lugares ser preenchido;");

        if (string.IsNullOrWhiteSpace(Senha))
            erros.Add("O Campo Senha precisa ser preenchido;");

        return erros;
    }
}
