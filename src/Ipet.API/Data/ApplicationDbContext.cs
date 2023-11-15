using Ipet.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ipet.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
                //options.UseMySql("server=mysql-banco-api.mysql.database.azure.com;initial catalog = IPET;uid=MysqlRoot;pwd=Mudar#123",
            options.UseMySql("server=localhost;initial catalog = ipet;uid=root;pwd=root",
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    } 
}
