﻿
using Ipet.Domain.Models;
using Ipet.Service.Models;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseStore.Data.Mappings
{
    public class ServicoMapping : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Imagem)
                //.IsRequired()
                .HasColumnType("LONGTEXT");
            builder.Property(p => p.Estabelecimento)
                //.IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(p => p.Hashtags)
                .WithOne(h => h.servico)
                .HasForeignKey(h => h.IdServico)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Servicos");
        }
    }
}