using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloConta;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

public class ContaController(
    ServicoConta servicoConta,
    IMapper mapeador
) : Controller
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

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarContaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarContaDto dto =
            mapeador.Map<CadastrarContaDto>(cadastrarVm);

        Result resultado = servicoConta.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

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

        EditarContaViewModel editarVm =
            mapeador.Map<EditarContaViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarContaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarContaDto dto =
            mapeador.Map<EditarContaDto>(editarVm);

        Result resultado = servicoConta.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

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
    public ActionResult Excluir(ExcluirContaViewModel excluirVm)
    {
        Result resultado = servicoConta.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}