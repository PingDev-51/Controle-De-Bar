using AutoMapper;

using ControleDeBar.Aplicacao.Modulos.ModuloConta;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

public class ContaProfile : Profile
{
    public ContaProfile()
    {
        CreateMap<ListarContaDto, ListarContaViewModel>();

        CreateMap<CadastrarContaViewModel, CadastrarContaDto>();
        CreateMap<EditarContaViewModel, EditarContaDto>();
        CreateMap<DetalhesContaDto, EditarContaViewModel>();
        CreateMap<DetalhesContaDto, DetalhesContaViewModel>();
        CreateMap<DetalhesContaDto, ExcluirContaViewModel>();
        CreateMap<OpcaoGarconDto, OpcaoGarconViewModel>();
        CreateMap<OpcaoMesaDto, OpcaoMesaViewModel>();
    }
}