using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;

namespace ApiLoja.DTO
{

    // DTO para Compra (classe intermediária entre Cliente e Produto)
    public class CompraDTO
    {
        public CompraDTO() { }

        public CompraDTO(Compra obj)
        {
            this.Id = obj.Id.ToString();
            this.ProdutoId = obj.ProdutoId;
            this.ClienteId = obj.ClienteId;
            this.Produto = new ProdutoDTO(obj.Produto); // Incluir detalhes do produto
            this.Quantidade = obj.Quantidade;
        }

        public string Id { get; set; }
        public long ProdutoId { get; set; }
        public long ClienteId { get; set; }
        public ProdutoDTO Produto { get; set; } = new ProdutoDTO(); // DTO do produto relacionado
        public int Quantidade { get; set; }

        public Compra GetModel()
        {
            var model = new Compra();
            PreencherModel(model);
            return model;
        }

        public void PreencherModel(Compra obj)
        {
            long.TryParse(this.Id, out long id);
            obj.Id = id;
            obj.ClienteId = this.ClienteId;
            obj.ProdutoId = this.ProdutoId;
            obj.Produto = this.Produto.GetModel(); // Associar o produto ao modelo
            obj.Quantidade = this.Quantidade;
        }
    }
}