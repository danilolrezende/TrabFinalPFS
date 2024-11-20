using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;

namespace ApiLoja.Services
{
    public class TokenServices
    {
        public string GerarToken(Usuario user)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(GetTokenDescription(user));

            return handler.WriteToken(token);
        }

        private static SecurityTokenDescriptor GetTokenDescription(Usuario user)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = GerarClaims(user),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = GetCredentials(),
            };
        }

        private static SigningCredentials GetCredentials()
        {
            var key = Encoding.ASCII.GetBytes(Config.Instancia.ChavePrivada ?? "");

            return new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            );
        }

        private static ClaimsIdentity GerarClaims(Usuario user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            ci.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            return ci;
        }
    }
}