using Dietcode.Core.Lib;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace FluxoCaixa.Test.Lancamento
{
    public class TestLancamento
    {
        //private readonly string urlDebito = "https://localhost:5610/api/v1/Lancamentos/Debito";
        //private readonly string urlCredito = "https://localhost:5610/api/v1/Lancamentos/Credito";
        private readonly string urlDebito = "https://fc-lancamento.dietcode.com.br/api/v1/Lancamentos/Debito";
        private readonly string urlCredito = "https://fc-lancamento.dietcode.com.br/api/v1/Lancamentos/Credito";

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
    }
}