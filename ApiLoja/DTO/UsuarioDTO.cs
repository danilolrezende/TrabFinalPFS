using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLoja.Models;

namespace ApiLoja.DTO
{
    public class UsuarioDTO
    {
        public UsuarioDTO() { }

        public UsuarioDTO(Usuario obj)
        {
            this.Id = obj.Id.ToString();
            this.Username = obj.Username;
        }

        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public Usuario GetModel()
        {
            var model = new Usuario();
            PreencherModel(model);
            return model;
        }

        public void PreencherModel(Usuario obj)
        {
            long.TryParse(this.Id, out long id);
            obj.Id = id;
            obj.Username = this.Username;
        }
    }
}