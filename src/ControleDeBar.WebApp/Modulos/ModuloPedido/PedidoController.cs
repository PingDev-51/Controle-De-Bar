using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloPedido;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloPedido;

public class PedidoController(
    ServicoPedido servicoPedido,
    IMapper mapeador
) : Controller
{
    [HttpGet]
    public ActionResult Listar(Guid contaId)
    {
        List<ListarPedidoDto> dtos =
            servicoPedido.SelecionarPorConta(contaId);

        List<ListarPedidoViewModel> listarVms =
            mapeador.Map<List<ListarPedidoViewModel>>(dtos);

        ViewBag.ContaId = contaId;

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid contaId)
    {
        List<OpcaoProdutoDto> dtos =
            servicoPedido.SelecionarProdutos();

        List<OpcaoProdutoViewModel> produtos =
            mapeador.Map<List<OpcaoProdutoViewModel>>(dtos);

        ViewBag.Produtos = produtos;
        ViewBag.ContaId = contaId;

        CadastrarPedidoViewModel cadastrarVm = new(
            Guid.Empty,
            Guid.Empty,
            0
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(
     Guid contaId,
     CadastrarPedidoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarProdutos();

            ViewBag.ContaId = contaId;

            return View(cadastrarVm);
        }

        CadastrarPedidoDto dto = new(
            contaId,
            cadastrarVm.ProdutoId,
            cadastrarVm.Quantidade
        );

        Result resultado = servicoPedido.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            CarregarProdutos();

            ViewBag.ContaId = contaId;

            return View(cadastrarVm);
        }

        return RedirectToAction(
            nameof(Listar),
            new { contaId }
        );
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesPedidoDto> resultado =
            servicoPedido.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarPedidoViewModel editarVm =
            mapeador.Map<EditarPedidoViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarPedidoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarPedidoDto dto =
            mapeador.Map<EditarPedidoDto>(editarVm);

        Result resultado =
            servicoPedido.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(
            nameof(Listar),
            new { contaId = editarVm.ContaId }
        );
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesPedidoDto> resultado =
            servicoPedido.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirPedidoViewModel excluirVm =
            mapeador.Map<ExcluirPedidoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPedidoViewModel excluirVm)
    {
        Result resultado =
            servicoPedido.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(
            nameof(Listar),
            new { contaId = excluirVm.ContaId }
        );
    }

    private void CarregarProdutos()
    {
        List<OpcaoProdutoDto> dtos =
            servicoPedido.SelecionarProdutos();

        ViewBag.Produtos =
            mapeador.Map<List<OpcaoProdutoViewModel>>(dtos);
    }
}