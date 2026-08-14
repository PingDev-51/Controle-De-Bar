using System;
using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.Modulos.ModuloDono;

public class Dono : EntidadeBase<Dono>
{


    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    // public Estabelecimento Estabelecimento { get; set; } = string.Empty;

    public Dono() { }

    public Dono(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    public override void Atualizar(Dono entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Email = entidadeAtualizada.Email;
        Senha = entidadeAtualizada.Senha;
    }

    public override List<string> Validar()
    {
        List<string> erros = new();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("Este campo Nome precisa ser preenchido");
        else if (Nome.Length < 2)
            erros.Add("O campo Nome precisa ter mais que 2 caracteres");
        else if (Nome.Length > 100)
            erros.Add("O campo Nome precisa ter menos que 100 caracteres");

        if (string.IsNullOrWhiteSpace(Email))
            erros.Add("Este campo Email precisa ser preenchido");

        if (string.IsNullOrWhiteSpace(Senha))
            erros.Add("Este campo Senha precisa ser preenchido");

        return erros;
    }
}
