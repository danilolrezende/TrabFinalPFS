using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Compra>? Compras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>()
                        .HasOne(c => c.Cliente)
                        .WithMany()
                        .HasForeignKey(c => c.ClienteId);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Produto)
                .WithMany()
                .HasForeignKey(c => c.ProdutoId);
        }
    }
}