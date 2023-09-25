
using EnterpriseStore.Domain.Models;
using EnterpriseStore.Service.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseStore.Data.Mappings
{
    public class CarrinhoMapping : IEntityTypeConfiguration<Carrinho>
    {
        public void Configure(EntityTypeBuilder<Carrinho> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("Carrinho");
        }
    }
}