using AutoMapper;
using Ipet.Api.ViewModels;
using Ipet.Domain.Models;

namespace Ipet.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CarrinhoProduto, CarrinhoProdutoViewModel>().ReverseMap();
            CreateMap<Carrinho, CarrinhoViewModel>().ReverseMap();
            CreateMap<PerfilPet, PerfilPetViewModel>().ReverseMap();
            CreateMap<ProdutoHashtag, ProdutoHashtagViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<ServiçoHashtag, ServiçoHashtagViewModel>().ReverseMap();
            CreateMap<Servico, ServicoViewModel>().ReverseMap();

        }
    }
}
