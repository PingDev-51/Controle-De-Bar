using System;
using System.Collections.Generic;
using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.Dominio.Modulos.ModuloContas;

public class Conta : EntidadeBase<Conta>
{
    public Guid UserId { get; set; }
    public Garcon? Garcon { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public DateTime DataAbertura { get; set; }
    public Situacao Situacao { get; set; }
    public Mesa? Mesa { get; set; }

    public Conta() { }

    public Conta(string nomeCliente) : this()
    {
        NomeCliente = nomeCliente;
        DataAbertura = DateTime.Now;
        Situacao = Situacao.Aberta;
    }

    public override void Atualizar(Conta entidadeAtualizada)
    {
        NomeCliente = entidadeAtualizada.NomeCliente;
        DataAbertura = entidadeAtualizada.DataAbertura;
        Situacao = entidadeAtualizada.Situacao;
        Garcon = entidadeAtualizada.Garcon;
        Mesa = entidadeAtualizada.Mesa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(NomeCliente))
            erros.Add("O campo NomeCliente precisa ser preenchido.");

        if (Garcon == null)
            erros.Add("O garçom precisa ser informado.");

        if (Mesa == null)
            erros.Add("A mesa precisa ser informada.");

        return erros;
    }
}