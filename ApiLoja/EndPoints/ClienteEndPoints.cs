/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.DTO;
using ApiLoja.Infra;
using ApiLoja.Models;
using Microsoft.EntityFrameworkCore;


public static class ClienteEndPoints
{

    public static void AdicionarClienteEndPoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/clientes");
        grupo.MapGet("/", GetAsync);
        grupo.MapGet("/{id}", GetByIdAsync);
        grupo.MapPost("", PostAsync);
        grupo.MapPut("/{id}", PutAsync);
        grupo.MapDelete("/{id}", DeleteAsync);
    }

    private static async Task<IResult> GetAsync(AppDbContext db)
    {
        var objetos = await db.Clientes.ToListAsync();
        return TypedResults.Ok(objetos.Select(x => new ClienteDTO(x)));
    }

    private static async Task<IResult> GetByIdAsync(string id, AppDbContext db)
    {
        var obj = await db.Clientes.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(new ClienteDTO(obj));
    }

    private static async Task<IResult> PostAsync(ClienteDTO dto, AppDbContext db)
    {
        Cliente obj = dto.GetModel();
        obj.Id = GeradorId.GetId();
        await db.Clientes.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"cliente/{obj.Id}", new ClienteDTO(obj));
    }

    private static async Task<IResult> PutAsync(string id, ClienteDTO dto, AppDbContext db)
    {
        if (id != dto.Id)
        {
            return TypedResults.BadRequest();
        }

        var obj = await db.Clientes.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        dto.PreencherModel(obj);

        db.Clientes.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string id, AppDbContext db)
    {
        var obj = await db.Clientes.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        db.Clientes.Remove(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }


}
 */