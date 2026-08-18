using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloPedido;

namespace ControleDeBar.WebApp.Modulos.ModuloPedido;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        CreateMap<ListarPedidoDto, ListarPedidoViewModel>();
        CreateMap<CadastrarPedidoViewModel, CadastrarPedidoDto>();
        CreateMap<EditarPedidoViewModel, EditarPedidoDto>();
        CreateMap<DetalhesPedidoDto, EditarPedidoViewModel>();
        CreateMap<DetalhesPedidoDto, ExcluirPedidoViewModel>();
        CreateMap<OpcaoProdutoDto, OpcaoProdutoViewModel>();
    }
}