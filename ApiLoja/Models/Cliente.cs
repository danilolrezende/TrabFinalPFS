using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLoja.Models
{
    public class Cliente
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();

    }
}