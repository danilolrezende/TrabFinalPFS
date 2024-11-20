using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.DTO;
using ApiLoja.Infra;
using ApiLoja.Models;
using ApiLoja.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.EndPoints
{
    public static class LoginEndPoint
    {

        public static void AdicionarLoginEndPoints(this WebApplication app)
        {
            var grupo = app.MapGroup("/login");

            grupo.MapPost("/app", PostLoginAsync);
            grupo.MapPost("/navegador", PostLoginNavegadorAsync);
            grupo.MapGet("/navegador/logout", PostLogoutNavegador);
        }

        private static async Task<IResult> PostLogoutNavegador(HttpContext context)
        {
            context.Response.Cookies.Delete("accessToken");
            return TypedResults.Ok();
        }

        private static async Task<IResult> PostLoginNavegadorAsync(LoginDTO dto, AppDbContext db, IPasswordHasher<Usuario> hasher)
        {
            var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (usuario == null)
            {
                return TypedResults.Unauthorized();
            }
            else if (hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, dto.Senha) == PasswordVerificationResult.Failed)
            {
                return TypedResults.Unauthorized();
            }
            return TypedResults.Ok(new TokenServices().GerarToken(usuario));
        }

        private static async Task<IResult> PostLoginAsync(LoginDTO dto, AppDbContext db, HttpContext contexto, IPasswordHasher<Usuario> hasher)
        {
            var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (usuario == null)
            {
                return TypedResults.Unauthorized();
            }
            else if (hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, dto.Senha) == PasswordVerificationResult.Failed)
            {
                return TypedResults.Unauthorized();
            }
            var token = new TokenServices().GerarToken(usuario);

            contexto.Response.Cookies.Append(
                "accessToken",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                }
            );

            return TypedResults.Ok();
        }


    }
}