using AutoMapper;
using EnterpriseStore.Domain.Models;
using EnterpriseStore.MVC.ViewModels;
using EnterpriseStore.Service.Models;
using Ipet.Domain.Models;

namespace EnterpriseStore.MVC.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}