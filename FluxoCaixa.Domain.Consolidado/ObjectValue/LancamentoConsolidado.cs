using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Domain.Consolidado.ObjectValue
{
    public class LancamentoConsolidado
    {
        public LancamentoConsolidado()
        {
            Lancamentos = new List<Lancamentos>();
        }
        public decimal Debitos { get; set; }
        public decimal Creditos { get; set; }
        public decimal Saldo { get; set; }
        public List<Lancamentos> Lancamentos { get; set; }
    }
}
