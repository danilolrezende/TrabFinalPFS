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
        //public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

/*         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento entre Produto e Cliente
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.SetNull); // Deixa o campo ClienteId nulo se o cliente for deletado
        } */
    }
}