using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLoja
{
    public class Config
    {
        public string? ChavePrivada { get; set; }

        public static Config Instancia => instancia ??= new Config
        {
            ChavePrivada = Environment.GetEnvironmentVariable("AUTH_CHAVE_PRIVADA")
        };

        private Config(){}
        private static Config? instancia = null;
        
    }
}