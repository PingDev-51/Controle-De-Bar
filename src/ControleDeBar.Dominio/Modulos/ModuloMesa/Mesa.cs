using System;
using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.Compartilhado.Identity;

namespace ControleDeBar.Dominio.Modulos.ModuloMesa;

public class Mesa : EntidadeBase<Mesa>, IEntidadeDoUsuario
{
    public Guid UserId { get; set; }
    public string NumeroDaMesa { get; set; } = string.Empty;
    public string QuantidadeDeLugares { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public StatusDaMesa StatusDaMesa { get; set; }

    public Mesa() { }

    public Mesa(string numeroDaMesa, string quantidadeDeLugares, StatusDaMesa statusDaMesa)
    {
        NumeroDaMesa = numeroDaMesa;
        QuantidadeDeLugares = quantidadeDeLugares;
        StatusDaMesa = statusDaMesa;
    }

    public override void Atualizar(Mesa entidadeAtualizada)
    {
        NumeroDaMesa = entidadeAtualizada.NumeroDaMesa;
        QuantidadeDeLugares = entidadeAtualizada.QuantidadeDeLugares;
        StatusDaMesa = entidadeAtualizada.StatusDaMesa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(NumeroDaMesa))
            erros.Add("O Campo Numero Da Mesa precisa ser preenchido;");

        if (string.IsNullOrWhiteSpace(QuantidadeDeLugares))
            erros.Add("O Campo Quantidade De Lugares ser preenchido;");

        return erros;
    }
}
