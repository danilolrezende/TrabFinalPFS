using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.DTO;
using ApiLoja.Infra;
using ApiLoja.Models;
using Microsoft.EntityFrameworkCore;


public static class ProdutoEndPoints
{
    public static void AdicionarProdutoEndPoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/produtos").RequireAuthorization();
        //var grupo = app.MapGroup("/produtos");

        grupo.MapGet("/", GetAsync);
        grupo.MapGet("/{id}", GetByIdAsync);
        grupo.MapPost("", PostAsync);
        grupo.MapPut("/{id}", PutAsync);
        grupo.MapDelete("/{id}", DeleteAsync);
    }

    private static async Task<IResult> GetAsync(AppDbContext db)
    {
        var objetos = await db.Produtos.ToListAsync();
        return TypedResults.Ok(objetos.Select(x => new ProdutoDTO(x)));
    }

    private static async Task<IResult> GetByIdAsync(string id, AppDbContext db)
    {
        var obj = await db.Produtos.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(new ProdutoDTO(obj));
    }



    private static async Task<IResult> PostAsync(ProdutoDTO dto, AppDbContext db)
    {
        Produto obj = dto.GetModel();
        obj.Id = GeradorId.GetId();
        await db.Produtos.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"produto/{obj.Id}", new ProdutoDTO(obj));
    }

    private static async Task<IResult> PutAsync(string id, ProdutoDTO dto, AppDbContext db)
    {
        if (id != dto.Id)
        {
            return TypedResults.BadRequest();
        }

        var obj = await db.Produtos.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        dto.PreencherModel(obj);

        db.Produtos.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string id, AppDbContext db)
    {
        var obj = await db.Produtos.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        db.Produtos.Remove(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

}
