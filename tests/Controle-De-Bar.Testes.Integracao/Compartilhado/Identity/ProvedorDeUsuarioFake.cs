using ControleDeBar.Dominio.Compartilhado.Identity;

namespace Controle_De_Bar.Testes.Integracao.Compartilhado.Identity;

public sealed class ProvedorDeUsuarioFake(Guid id) : IProvedorDeUsuario
{
    public Guid? Id { get; } = id;

    public bool EstaAutenticado => true;
}