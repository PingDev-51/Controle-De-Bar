using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloConta;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

public class ContaController(ServicoConta servicoConta, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarContaDto> dtos = servicoConta.SelecionarTodos();

        List<ListarContaViewModel> listarVms =
            mapeador.Map<List<ListarContaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarContaViewModel cadastrarVm = new(
            string.Empty,
            Guid.Empty,
            Guid.Empty
        );

        ViewBag.Garcons = SelecionarGarcon();
        ViewBag.Mesas = SelecionarMesa();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarContaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Garcons = SelecionarGarcon();
            ViewBag.Mesas = SelecionarMesa();

            return View(cadastrarVm);
        }

        CadastrarContaDto dto =
            mapeador.Map<CadastrarContaDto>(cadastrarVm);

        Result resultado = servicoConta.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ViewBag.Garcons = SelecionarGarcon();
            ViewBag.Mesas = SelecionarMesa();

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesContaDto> resultado =
            servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarContaViewModel editarVm = new(
            resultado.Value.Id,
            resultado.Value.NomeCliente,
            resultado.Value.GarconId,
            resultado.Value.MesaId,
            resultado.Value.Situacao
        );

        ViewBag.Garcons = SelecionarGarcon();
        ViewBag.Mesas = SelecionarMesa();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarContaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Garcons = SelecionarGarcon();
            ViewBag.Mesas = SelecionarMesa();

            return View(editarVm);
        }

        EditarContaDto dto =
            mapeador.Map<EditarContaDto>(editarVm);

        Result resultado = servicoConta.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ViewBag.Garcons = SelecionarGarcon();
            ViewBag.Mesas = SelecionarMesa();

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Detalhes(Guid id)
    {
        Result<DetalhesContaDto> resultado =
            servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesContaViewModel detalhesVm =
            mapeador.Map<DetalhesContaViewModel>(resultado.Value);

        return View(detalhesVm);
    }

    [HttpGet]
    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesContaDto> resultado =
            servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirContaViewModel excluirVm =
            mapeador.Map<ExcluirContaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    [HttpPost]
    public ActionResult Excluir(ExcluirContaViewModel excluirVm)
    {
        Result resultado = servicoConta.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    public List<OpcaoGarconViewModel> SelecionarGarcon()
    {
        List<OpcaoGarconDto> dtos =
            servicoConta.SelecionarGarcon();

        return mapeador.Map<List<OpcaoGarconViewModel>>(dtos);
    }

    public List<OpcaoMesaViewModel> SelecionarMesa()
    {
        List<OpcaoMesaDto> dtos =
            servicoConta.SelecionarMesa();

        return mapeador.Map<List<OpcaoMesaViewModel>>(dtos);
    }
}