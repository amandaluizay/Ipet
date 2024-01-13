
using Ipet.Domain.Models;
using Ipet.Service.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ipet.Service.Services;

namespace Ipet.Data.Mappings
{
    public class ServiçoHashtagMapping : IEntityTypeConfiguration<ServiçoHashtag>
    {
        public void Configure(EntityTypeBuilder<ServiçoHashtag> builder)
        {
            builder.HasKey(p => p.Id);



            builder.Property(p => p.Tag)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasConversion(tag => tag.ToUpper(), tag => tag);

            builder.ToTable("ServiçoHashtag");
        }
    }
}