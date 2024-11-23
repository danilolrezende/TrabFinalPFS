using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLoja.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double Preco { get; set; }
        public int Estoque { get; set; }
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();

    }
}