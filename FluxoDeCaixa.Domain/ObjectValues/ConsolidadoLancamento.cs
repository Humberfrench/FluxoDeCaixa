using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Domain.ObjectValues
{
    public class ConsolidadoLancamento
    {
        public DateTime Data { get; set; }
        public decimal ConsolidadoDebito { get; set; }
        public decimal ConsolidadoCredito { get; set; }
        public decimal SaldoDoDia { get; set; }

    }
}
