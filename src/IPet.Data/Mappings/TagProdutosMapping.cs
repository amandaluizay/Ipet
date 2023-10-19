
using Ipet.Domain.Models;
using Ipet.Service.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class TagProdutoMapping : IEntityTypeConfiguration<TagProduto>
    {
        public void Configure(EntityTypeBuilder<TagProduto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(t => t.Produto)
                .WithOne(p => p.TagProduto)
                .HasForeignKey<TagProduto>(t => t.IdProduto);

            builder.HasMany(t => t.Hashtags)
          .WithOne()
          .HasForeignKey("TagProdutoId");

            builder.ToTable("Tagproduto");
        }
    }
}