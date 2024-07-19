using Newtonsoft.Json;

namespace FluxoCaixa.Api.Config
{
    public class Configuration
    {
        public Configuration()
        {
            Ambiente = "";
            DataBase = "";
        }

        [JsonProperty("Ambiente")]
        public string Ambiente { get; set; }

        [JsonProperty("DataBase")]
        public string DataBase { get; set; }

    }
}
