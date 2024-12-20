using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLoja
{
    public static class DotEnv
    {
        public static void Carregar(string caminho)
        {
            if (!File.Exists(caminho))
            {
                return;
            }
            var linhas = File.ReadAllLines(caminho);

            foreach (var linha in linhas)
            {
                var item = linha.Split('=',
                    StringSplitOptions.RemoveEmptyEntries
                );

                if (item.Length!= 2)
                {
                    continue;
                }
                Environment.SetEnvironmentVariable(item[0], item[1]);
            }
        }
    }
}