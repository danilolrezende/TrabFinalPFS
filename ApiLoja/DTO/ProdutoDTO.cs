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
            this.Preco = obj.Preco;
            this.Categoria = obj.Categoria;
            this.Estoque = obj.Estoque;
        }

        public string Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public double Preco { get; set; }
        public string Categoria { get; set; }
        public int Estoque { get; set; }

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
            obj.Preco = this.Preco;
            this.Categoria = obj.Categoria;
            this.Estoque = obj.Estoque;
        }

    }
}