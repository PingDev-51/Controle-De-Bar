using System;
using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloMesa;

public class MesaController(ServicoMesa servicoMesa, IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMesaDto> dtos = servicoMesa.SelecionarTodos();

        List<ListarMesaViewModel> listarVms = mapeador.Map<List<ListarMesaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMesaViewModel cadastrarVm = new(string.Empty, string.Empty, 0);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMesaViewModel cadastrarVm)
    {

        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarMesaDto dto = mapeador.Map<CadastrarMesaDto>(cadastrarVm);

        Result resultado = servicoMesa.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
