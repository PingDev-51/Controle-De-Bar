using System;
using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.Modulos.ModuloGarcon;

public class Garcon : EntidadeBase<Garcon>
{
    public string Nome { get; set; } = string.Empty;
    //Add o estabelecimento mais tarde

    public Garcon() { }

    public Garcon(string nome)
    {
        Nome = nome;
    }

    public override void Atualizar(Garcon entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
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

        return erros;
    }
}
