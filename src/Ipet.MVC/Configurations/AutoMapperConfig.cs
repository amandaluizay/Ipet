using AutoMapper;
using Ipet.Domain.Models;
using Ipet.ViewModels;

namespace Ipet.MVC.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Servico, ServicoViewModel>().ReverseMap();
            CreateMap<Carrinho, CarrinhoViewModel>().ReverseMap();
            CreateMap<PerfilPet, PerfilPetViewModel>().ReverseMap();
            CreateMap<ProdutoHashtag, ProdutoHashtagViewModel>().ReverseMap();
            CreateMap<ServiçoHashtag, ServiçoHashtagViewModel>().ReverseMap();
        }
    }
}