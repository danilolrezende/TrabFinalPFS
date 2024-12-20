﻿// <auto-generated />
using ApiLoja.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiLoja.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("ApiLoja.Models.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ApiLoja.Models.Compra", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ClienteId1")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ProdutoId1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ClienteId1");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("ProdutoId1");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("ApiLoja.Models.Produto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Estoque")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Preco")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ApiLoja.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ApiLoja.Models.Compra", b =>
                {
                    b.HasOne("ApiLoja.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiLoja.Models.Cliente", null)
                        .WithMany("Compras")
                        .HasForeignKey("ClienteId1");

                    b.HasOne("ApiLoja.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiLoja.Models.Produto", null)
                        .WithMany("Compras")
                        .HasForeignKey("ProdutoId1");

                    b.Navigation("Cliente");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("ApiLoja.Models.Cliente", b =>
                {
                    b.Navigation("Compras");
                });

            modelBuilder.Entity("ApiLoja.Models.Produto", b =>
                {
                    b.Navigation("Compras");
                });
#pragma warning restore 612, 618
        }
    }
}
