using Ipet.API.Extensions;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Notificacoes;

namespace Ipet.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

  

            return services;
        }
    }
}
