using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLoja.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Username { get; set; }  = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

    }
}