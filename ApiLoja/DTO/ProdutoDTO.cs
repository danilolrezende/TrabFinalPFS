using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;

namespace ApiLoja.DTO
{
    public class ProdutoDTO
    {
        public ProdutoDTO() { }

        public ProdutoDTO(Produto obj)
        {
            this.Id = obj.Id.ToString();
            this.Nome = obj.Nome;
            this.Categoria = obj.Categoria;
            this.Preco = obj.Preco;
            this.Estoque = obj.Estoque;
            //this.ClienteId = obj.ClienteId?.ToString();
        }

        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double Preco { get; set; }
        public int Estoque { get; set; }
        //public string? ClienteId { get; set; }

        public Produto GetModel()
        {
            var model = new Produto();
            PreencherModel(model);
            return model;
        }

        public void PreencherModel(Produto obj)
        {
            long.TryParse(this.Id, out long id);
            obj.Id = id;
            obj.Nome = this.Nome;
            obj.Categoria = this.Categoria;
            obj.Preco = this.Preco;
            obj.Estoque = this.Estoque;

            /* int.TryParse(this.ClienteId, out int clienteId);
            obj.ClienteId = clienteId; */
        }

    }
}