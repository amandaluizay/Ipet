
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class tagProdutoMapping : IEntityTypeConfiguration<TagProduto>
    {
        public void Configure(EntityTypeBuilder<TagProduto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("TagProduto");
        }
    }
}