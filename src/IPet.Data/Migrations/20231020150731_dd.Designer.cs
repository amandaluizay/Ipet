﻿// <auto-generated />
using System;
using Ipet.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ipet.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20231020150731_dd")]
    partial class dd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ipet.Domain.Models.Carrinho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Carrinho", (string)null);
                });

            modelBuilder.Entity("Ipet.Domain.Models.CarrinhoProduto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CarrinhoId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarrinhoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("CarrinhoProduto", (string)null);
                });

            modelBuilder.Entity("Ipet.Domain.Models.Hashtag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdServico")
                        .HasColumnType("char(36)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdProduto");

                    b.HasIndex("IdServico");

                    b.ToTable("Hashtag", (string)null);
                });

            modelBuilder.Entity("Ipet.Domain.Models.PerfilPet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("char(36)");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Porte")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Raca")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TipoAnimal")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PerfilPet", (string)null);
                });

            modelBuilder.Entity("Ipet.Domain.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Estabelecimento")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("EstabelecimentoId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("LONGTEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("Ipet.Domain.Models.Servico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Estabelecimento")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("EstabelecimentoId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("LONGTEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Servicos", (string)null);
                });

            modelBuilder.Entity("Ipet.Domain.Models.CarrinhoProduto", b =>
                {
                    b.HasOne("Ipet.Domain.Models.Carrinho", "Carrinho")
                        .WithMany("CarrinhoProdutos")
                        .HasForeignKey("CarrinhoId")
                        .IsRequired();

                    b.HasOne("Ipet.Domain.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .IsRequired();

                    b.Navigation("Carrinho");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Ipet.Domain.Models.Hashtag", b =>
                {
                    b.HasOne("Ipet.Domain.Models.Produto", "produto")
                        .WithMany("Hashtags")
                        .HasForeignKey("IdProduto")
                        .IsRequired();

                    b.HasOne("Ipet.Domain.Models.Servico", "servico")
                        .WithMany("Hashtags")
                        .HasForeignKey("IdServico")
                        .IsRequired();

                    b.Navigation("produto");

                    b.Navigation("servico");
                });

            modelBuilder.Entity("Ipet.Domain.Models.Carrinho", b =>
                {
                    b.Navigation("CarrinhoProdutos");
                });

            modelBuilder.Entity("Ipet.Domain.Models.Produto", b =>
                {
                    b.Navigation("Hashtags");
                });

            modelBuilder.Entity("Ipet.Domain.Models.Servico", b =>
                {
                    b.Navigation("Hashtags");
                });
#pragma warning restore 612, 618
        }
    }
}