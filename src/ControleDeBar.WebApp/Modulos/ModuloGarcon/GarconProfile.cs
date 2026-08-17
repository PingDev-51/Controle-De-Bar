using System;
using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloGarcon;
using ControleDeBar.Dominio.Modulos.ModuloGarcon;

namespace ControleDeBar.WebApp.Modulos.ModuloGarcon;

public class GarconProfile : Profile
{

    public GarconProfile()
    {
        CreateMap<ListarGarconDto, ListarGarconViewModel>();
        CreateMap<CadastrarGarconViewModel, CadastrarGarconDto>();
        CreateMap<EditarGarconViewModel, EditarGarconDto>();
        CreateMap<DetalhesGarconDto, EditarGarconViewModel>();
        CreateMap<DetalhesGarconDto, ExcluirGarconViewModel>();
    }

}
