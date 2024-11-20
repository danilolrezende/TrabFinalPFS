/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;

namespace ApiLoja.DTO
{
    public class ClienteDTO
    {
        public ClienteDTO() { }

        public ClienteDTO(Cliente obj)
        {
            this.Id = obj.Id.ToString();
            this.Nome = obj.Nome;
            this.Email = obj.Email;
            this.Produtos = obj.Produtos?.Select(p => new ProdutoDTO(p)).ToList();  // Relacionamento com produtos
        }

        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<ProdutoDTO> Produtos { get; set; } = new List<ProdutoDTO>(); // Lista de produtos

        public Cliente GetModel()
        {
            var model = new Cliente();
            PreencherModel(model);
            return model;
        }

        public void PreencherModel(Cliente obj)
        {
            long.TryParse(this.Id, out long id);
            obj.Id = id;
            obj.Nome = this.Nome;
            obj.Email = this.Email;
            obj.Produtos = this.Produtos.Select(p => p.GetModel()).ToList();  // Convertendo produtos

        }
    }
} */