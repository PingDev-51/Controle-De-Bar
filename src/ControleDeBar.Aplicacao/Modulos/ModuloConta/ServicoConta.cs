using ControleDeBar.Aplicacao.Compartilhado;
using ControleDeBar.Aplicacao.Modulos.ModuloConta;
using ControleDeBar.Dominio.Modulos.ModuloContas;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using FluentResults;

namespace ControleDeBar.Aplicacao.Modulos.ModuloConta;

public class ServicoConta : ServicoBase<Conta>
{
    private readonly IRepositorioContas repositorioContas;
    private readonly IRepositorioGarcon repositorioGarcon;
    private readonly IRepositorioMesa repositorioMesa;

    public ServicoConta(
        IRepositorioContas repositorioContas,
        IRepositorioGarcon repositorioGarcon,
        IRepositorioMesa repositorioMesa)
    {
        this.repositorioContas = repositorioContas;
        this.repositorioGarcon = repositorioGarcon;
        this.repositorioMesa = repositorioMesa;
    }

    public Result Cadastrar(CadastrarContaDto dto)
    {
        Garcon? garconSelecionado =
            repositorioGarcon.SelecionarPorId(dto.GarconId);

        if (garconSelecionado == null)
            return Falha(string.Empty, "Garçom não encontrado.");

        Mesa? mesaSelecionada =
            repositorioMesa.SelecionarPorId(dto.MesaId);

        if (mesaSelecionada == null)
            return Falha(string.Empty, "Mesa não encontrada.");

        Conta novaConta = new Conta(dto.NomeCliente)
        {
            Garcon = garconSelecionado,
            Mesa = mesaSelecionada
        };

        Result resultadoValidacao = ValidarEntidade(novaConta);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioContas.Cadastrar(novaConta);

        return Result.Ok();
    }

    public Result Editar(EditarContaDto dto)
    {
        Garcon? garconSelecionado =
            repositorioGarcon.SelecionarPorId(dto.GarconId);

        if (garconSelecionado == null)
            return Falha(string.Empty, "Garçom não encontrado.");

        Mesa? mesaSelecionada =
            repositorioMesa.SelecionarPorId(dto.MesaId);

        if (mesaSelecionada == null)
            return Falha(string.Empty, "Mesa não encontrada.");

        Conta contaAtualizada = new Conta(dto.NomeCliente)
        {
            Garcon = garconSelecionado,
            Mesa = mesaSelecionada,
            Situacao = dto.Situacao
        };

        Result resultadoValidacao = ValidarEntidade(contaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar =
            repositorioContas.Editar(dto.Id, contaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Conta não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Conta? conta = repositorioContas.SelecionarPorId(id);

        if (conta == null)
            return Falha(string.Empty, "Conta não encontrada.");

        repositorioContas.Excluir(id);

        return Result.Ok();
    }

    public List<ListarContaDto> SelecionarTodos()
    {
        return repositorioContas
            .SelecionarTodos()
            .Select(c => new ListarContaDto(
                c.Id,
                c.NomeCliente,
                c.Garcon!.Id,
                c.Garcon.Nome,
                c.Mesa!.Id,
                c.Mesa.NumeroDaMesa,
                c.DataAbertura,
                c.Situacao
            ))
            .ToList();
    }

    public Result<DetalhesContaDto> SelecionarPorId(Guid id)
    {
        Conta? conta = repositorioContas.SelecionarPorId(id);

        if (conta == null)
            return Result.Fail("Conta não encontrada.");

        if (conta.Garcon == null)
            return Result.Fail("O garçom da conta não foi encontrado.");

        if (conta.Mesa == null)
            return Result.Fail("A mesa da conta não foi encontrada.");

        return Result.Ok(new DetalhesContaDto(
            conta.Id,
            conta.NomeCliente,
            conta.Garcon.Id,
            conta.Garcon.Nome,
            conta.Mesa.Id,
            conta.Mesa.NumeroDaMesa,
            conta.DataAbertura,
            conta.Situacao
        ));
    }

    public List<OpcaoGarconDto> SelecionarGarcon()
    {
        return repositorioGarcon
            .SelecionarTodos()
            .Select(g => new OpcaoGarconDto(
                g.Id,
                g.Nome
            ))
            .ToList();
    }

    public List<OpcaoMesaDto> SelecionarMesa()
    {
        return repositorioMesa
            .SelecionarTodos()
            .Select(m => new OpcaoMesaDto(
                m.Id,
                m.NumeroDaMesa
            ))
            .ToList();
    }
}