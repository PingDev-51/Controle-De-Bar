using System;
using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloGarcon;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloGarcon;

public class GarconController(ServicoGarcon servicoGarcon, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarGarconDto> dtos = servicoGarcon.SelecionarTodos();

        List<ListarGarconViewModel> listarVms = mapeador.Map<List<ListarGarconViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarGarconViewModel cadastrarVm = new(string.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarGarconViewModel cadastrarVm)
    {

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarGarconDto dto = mapeador.Map<CadastrarGarconDto>(cadastrarVm);

        Result resultado = servicoGarcon.Cadastrar(dto);

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
        Result<DetalhesGarconDto> resultado = servicoGarcon.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarGarconViewModel editarVm = mapeador.Map<EditarGarconViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarGarconViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarGarconDto dto = mapeador.Map<EditarGarconDto>(editarVm);

        Result resultado = servicoGarcon.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
