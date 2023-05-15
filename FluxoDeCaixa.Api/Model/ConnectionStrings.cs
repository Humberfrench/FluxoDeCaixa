using Newtonsoft.Json;

namespace FluxoDeCaixa.Api.Model
{
    public class ConnectionStrings
    {
        public ConnectionStrings()
        {
            FluxoDeCaixaContext = string.Empty;
            ElmahContext = string.Empty;
        }

        [JsonProperty("FluxoDeCaixaContext")]
        public string FluxoDeCaixaContext { get; set; }

        [JsonProperty("ElmahContext")]
        public string ElmahContext { get; set; }
    }
}
