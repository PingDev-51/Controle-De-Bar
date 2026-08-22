using ControleDeBar.Dominio.Compartilhado.Identity;

namespace ControleDeBar.Testes.Integracao.Compartilhado.Identity;

public sealed class ProvedorDeUsuarioFake(Guid id) : IProvedorDeUsuario
{
    public Guid? Id { get; } = id;

    public bool EstaAutenticado => true;
}