using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Api.Config
{
    public class MyServices
    {
        public MyServices()
        {
            Worker = string.Empty;
            Lancamento = string.Empty;
            Consolidado = string.Empty;
        }

        public string Worker { get; set; }
        public string Lancamento { get; set; }
        public string Consolidado { get; set; }

    }
}
