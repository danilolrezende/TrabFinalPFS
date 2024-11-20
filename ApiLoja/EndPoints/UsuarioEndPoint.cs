using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.DTO;
using ApiLoja.Infra;
using ApiLoja.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.EndPoints
{
    public static class UsuarioEndPoint
    {
        public static void AdicionarUsuarioEndPoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/usuarios");

            grupo.MapGet("/", GetAsync).RequireAuthorization("Admin");
            grupo.MapGet("/{id}", GetByIdAsync).RequireAuthorization("Admin");
            grupo.MapPost("", PostAsync).RequireAuthorization("Admin");
            grupo.MapPost("/admin", PostAdminAsync).RequireAuthorization("Admin");
            grupo.MapPatch("/{id}/{senhaAnterior}/{senhaNova}", PatchAlterarSenhaAsync).RequireAuthorization("AdminOuCliente");
            grupo.MapPut("/{id}", PutAsync).RequireAuthorization("Admin").RequireAuthorization("Admin");
            grupo.MapDelete("/{id}", DeleteAsync).RequireAuthorization("Admin");
        }

        private static async Task<IResult> PatchAlterarSenhaAsync(string id, string senhaAnterior, string senhaNova, AppDbContext db, IPasswordHasher<Usuario> hasher, HttpContext contexto)
        {
            var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

            if (obj == null)
            {
                return TypedResults.NotFound();
            }

            if (!obj.Username.Equals(contexto?.User?.Identity?.Name))
            {
                return TypedResults.Forbid();
            }

            if (string.IsNullOrEmpty(obj.PasswordHash) || hasher.VerifyHashedPassword(obj, obj.PasswordHash, senhaAnterior) != PasswordVerificationResult.Failed)
            {
                obj.PasswordHash = hasher.HashPassword(obj, senhaNova);                
            }
            else
            {
                return TypedResults.Unauthorized();
            }

            db.Usuarios.Update(obj);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }


        private static async Task<IResult> GetAsync(AppDbContext db)
        {
            var objetos = await db.Usuarios.ToListAsync();
            return TypedResults.Ok(objetos.Select(x => new UsuarioDTO(x)));
        }

        private static async Task<IResult> GetByIdAsync(string id, AppDbContext db)
        {
            var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

            if (obj == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(new UsuarioDTO(obj));
        }



        private static async Task<IResult> PostAsync(UsuarioDTO dto, AppDbContext db)
        {
            Usuario obj = dto.GetModel();
            obj.Id = GeradorId.GetId();
            obj.Role = "cliente";
            await db.Usuarios.AddAsync(obj);
            await db.SaveChangesAsync();

            return TypedResults.Created($"usuario/{obj.Id}", new UsuarioDTO(obj));
        }

        private static async Task<IResult> PostAdminAsync(UsuarioDTO dto, AppDbContext db)
        {
            Usuario obj = dto.GetModel();
            obj.Id = GeradorId.GetId();
            obj.Role = "admin";
            await db.Usuarios.AddAsync(obj);
            await db.SaveChangesAsync();

            return TypedResults.Created($"usuario/{obj.Id}", new UsuarioDTO(obj));
        }

        private static async Task<IResult> PutAsync(string id, UsuarioDTO dto, AppDbContext db)
        {
            if (id != dto.Id)
            {
                return TypedResults.BadRequest();
            }

            var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

            if (obj == null)
            {
                return TypedResults.NotFound();
            }

            dto.PreencherModel(obj);

            db.Usuarios.Update(obj);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        private static async Task<IResult> DeleteAsync(string id, AppDbContext db)
        {
            var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

            if (obj == null)
            {
                return TypedResults.NotFound();
            }

            db.Usuarios.Remove(obj);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

    }
}