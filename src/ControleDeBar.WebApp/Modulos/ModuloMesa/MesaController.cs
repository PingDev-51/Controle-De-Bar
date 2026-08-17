using System;
using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
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

}
