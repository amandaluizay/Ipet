using Ipet.API.Extensions;
using Ipet.Data.Context;
using Ipet.Data.Repository;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Notificacoes;
using Ipet.Interfaces.Services;
using Ipet.Service.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Ipet.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<MeuDbContext>();
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
