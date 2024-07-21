using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Test.Lancamento
{
    public class PostData
    {
        public PostData()
        {
            Descricao = string.Empty;
            Observacao = string.Empty;
        }

        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

    }
}
