using Ipet.MVC.Data;
using Ipet.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.MVC.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                                    //options.UseMySql("server=mysql-banco-api.mysql.database.azure.com;initial catalog = IPET;uid=MysqlRoot;pwd=Mudar#123",
                                    options.UseMySql("server=164.152.244.159;initial catalog = ipet;uid=isaac;pwd=Isaacroque0209@;",
                                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
           


            return services;
        }
    }
}