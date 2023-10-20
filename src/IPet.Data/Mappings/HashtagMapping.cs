
using Ipet.Domain.Models;
using Ipet.Service.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ipet.Service.Services;

namespace Ipet.Data.Mappings
{
    public class HashtagMapping : IEntityTypeConfiguration<Hashtag>
    {
        public void Configure(EntityTypeBuilder<Hashtag> builder)
        {
            builder.HasKey(p => p.Id);



            builder.Property(p => p.Tag)
           .IsRequired()
           .HasColumnType("varchar(100)");

            builder.ToTable("Hashtag");
        }
    }
}