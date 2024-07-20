using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Test.Lancamento
{
    public class TestConsolidado
    {
        protected const string urlDia = "https://fc-consolidado.dietcode.com.br/api/v1/Consolidado/Dia";
        protected const string urlSevenDays = "https://fc-consolidado.dietcode.com.br/api/v1/Consolidado/SevenDays";
        protected const string urlMonth = "https://fc-consolidado.dietcode.com.br/api/v1/Consolidado/Month";
        protected const string urlMonth1 = "https://fc-consolidado.dietcode.com.br/api/v1/Consolidado/Month/{0}/{1}";
        protected const string urlRange = "https://fc-consolidado.dietcode.com.br/api/v1/Consolidado/Range/{0}/{1}";

        [Fact]
        public async Task TestAddLancamentoCredito()
        {

        }
    }
}
