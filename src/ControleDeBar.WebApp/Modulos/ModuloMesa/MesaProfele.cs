using System;
using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;

namespace ControleDeBar.WebApp.Modulos.ModuloMesa;

public class MesaProfele : Profile
{
    public MesaProfile()
    {
        CreateMap<ListarMesaDto, ListarMesaViewModel>();
        CreateMap<CadastrarMesaViewModel, CadastrarMesaDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();
        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>();
        CreateMap<DetalhesProdutoDto, ExcluirProdutoViewModel>();
    }
}
