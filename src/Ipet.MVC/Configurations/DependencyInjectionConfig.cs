using Ipet.Data.Context;
using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Notificacoes;
using Ipet.Interfaces.Services;
using Ipet.MVC.Extensions;
using Ipet.MVC.Services;
using Ipet.Service.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Ipet.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IServicoRepository, ServicoRepository>();

            services.AddScoped<ICarrinhoService, CarrinhoService>();
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

            services.AddScoped<IPerfilPetService, PerfilPetService>();
            services.AddScoped<IPerfilPetRepository, PerfilPetRepository>();

            services.AddScoped<IServiçoHashtagRepository, ServiçoHashtagRepository>();

            services.AddScoped<IProdutoHashtagRepository, ProdutoHashtagRepository>();


            return services;
        }
    }
}