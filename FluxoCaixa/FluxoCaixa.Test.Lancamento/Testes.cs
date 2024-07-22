using Dietcode.Core.Lib;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace FluxoCaixa.Test.Lancamento
{
    public class Testes
    {
        //private readonly string urlDebito = "https://localhost:5610/api/v1/Lancamentos/Debito";
        //private readonly string urlCredito = "https://localhost:5610/api/v1/Lancamentos/Credito";
        private readonly string urlDebito = "https://fc-lancamento.dietcode.com.br/api/v1/Lancamentos/Debito";
        private readonly string urlCredito = "https://fc-lancamento.dietcode.com.br/api/v1/Lancamentos/Credito";
        protected const string urlDia = "https://fc-consolidado.dietcode.com.br/api/v1/Query/Dia";
        protected const string urlSevenDays = "https://fc-consolidado.dietcode.com.br/api/v1/Query/SevenDays";
        protected const string urlMonth = "https://fc-consolidado.dietcode.com.br/api/v1/Query/Month";
        protected const string urlMonth1 = "https://fc-consolidado.dietcode.com.br/api/v1/Query/Month/{0}/{1}";
        protected const string urlRange = "https://fc-consolidado.dietcode.com.br/api/v1/Query/Range/{0}/{1}";
        private static string format = "yyyy-MM-dd";

        [Fact]
        public async Task TestAddLancamentoCredito()
        {
            using (var client = new HttpClient())
            {
                var postData = new PostData
                {
                    Descricao = "Teste",
                    Observacao = "Teste",
                    Valor = 100
                };

                var json = postData.ToJson();

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(urlCredito, stringContent);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [Fact]
        public async Task TestAddLancamentoDebito()
        {
            using (var client = new HttpClient())
            {
                var postData = new PostData
                {
                    Descricao = "Teste",
                    Observacao = "Teste",
                    Valor = 100
                };

                var json = postData.ToJson();

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(urlDebito, stringContent);
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }

        }

        [Fact]
        public async Task TestConsolidadoDia()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlDia);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestConsolidado7Dias()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlSevenDays);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task TestConsolidadoMes()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlMonth);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task TestConsolidadoMesEspec()
        {
            using (var client = new HttpClient())
            {
                var param = string.Format(urlMonth1, DateTime.Now.Month, DateTime.Now.Year);
                var response = await client.GetAsync(param);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task TestConsolidadoRange()
        {
            using (var client = new HttpClient())
            {
                var param = string.Format(urlRange, DateTime.Now.AddDays(-15).ToString(format), DateTime.Now.ToString(format));
                var response = await client.GetAsync(param);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

    }
}