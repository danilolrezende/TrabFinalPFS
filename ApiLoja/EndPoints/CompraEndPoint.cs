using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.DTO;
using ApiLoja.Infra;
using ApiLoja.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.EndPoints
{
    public static class CompraEndPoint
    {
        public static void AdicionarCompraEndPoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/compras").RequireAuthorization();

            // Endpoints para manipulação de compras
            grupo.MapGet("/", GetAsync);  // Listar todas as compras
            grupo.MapGet("/{id}", GetByIdAsync);  // Consultar uma compra por ID
            grupo.MapPost("", PostAsync);  // Criar uma nova compra
            grupo.MapDelete("/{id}", DeleteAsync);  // Deletar uma compra
            grupo.MapGet("/cliente/{clienteId}", GetByClienteIdAsync);  // Consultar compras por cliente
        }

        private static async Task<IResult> GetAsync(AppDbContext db)
        {
            var compras = await db.Compras
                .Include(c => c.Cliente)  // Carregar o Cliente
                .Include(c => c.Produto)  // Carregar o Produto
                .ToListAsync();

            return TypedResults.Ok(compras.Select(c => new CompraDTO(c)));
        }

        private static async Task<IResult> GetByIdAsync(string id, AppDbContext db)
        {
            var compra = await db.Compras
                .Include(c => c.Cliente)  // Carregar o Cliente
                .Include(c => c.Produto)  // Carregar o Produto
                .FirstOrDefaultAsync(c => c.Id == Convert.ToInt64(id));

            if (compra == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new CompraDTO(compra));
        }

        private static async Task<IResult> PostAsync(CompraDTO dto, AppDbContext db)
        {
            // Validar se o produto tem estoque suficiente
            var produto = await db.Produtos.FindAsync(dto.ProdutoId);
            if (produto == null)
            {
                return TypedResults.NotFound("Produto não encontrado.");
            }

            if (produto.Estoque < dto.Quantidade)
            {
                return TypedResults.BadRequest("Estoque insuficiente.");
            }

            // Criar a nova compra
            var compra = new Compra
            {
                ClienteId = dto.ClienteId,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade
            };

            db.Compras.Add(compra);

            // Decrementar o estoque do produto
            produto.Estoque -= dto.Quantidade;
            db.Produtos.Update(produto);

            await db.SaveChangesAsync();

            return TypedResults.Created($"compras/{compra.Id}", new CompraDTO(compra));
        }

        private static async Task<IResult> DeleteAsync(string id, AppDbContext db)
        {
            var compra = await db.Compras.FindAsync(Convert.ToInt64(id));

            if (compra == null)
            {
                return TypedResults.NotFound();
            }

            db.Compras.Remove(compra);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        private static async Task<IResult> GetByClienteIdAsync(string clienteId, AppDbContext db)
        {
            var compras = await db.Compras
                .Where(c => c.ClienteId == Convert.ToInt64(clienteId))
                .Include(c => c.Produto)  // Carregar o Produto
                .ToListAsync();

            if (!compras.Any())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(compras.Select(c => new CompraDTO(c)));
        }
    }
}