using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;

namespace ApiLoja.DTO
{
    public class ClienteDTO
    {
        public ClienteDTO() { }

        // Construtor para mapear do modelo Cliente para o DTO
        public ClienteDTO(Cliente obj)
        {
            this.Id = obj.Id.ToString();
            this.Nome = obj.Nome;
            this.Email = obj.Email;
            this.Compras = obj.Compras?.Select(c => new CompraDTO(c)).ToList(); // Agora estamos incluindo as compras, e não produtos diretamente
        }

        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Lista de compras, cada compra terá o produto associado
        public List<CompraDTO> Compras { get; set; } = new List<CompraDTO>();

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
            obj.Compras = this.Compras.Select(c => c.GetModel()).ToList(); // Preenche as compras a partir do DTO
        }
    }
}