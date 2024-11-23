using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLoja.Models
{
    public class Compra
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public long ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
    }
}