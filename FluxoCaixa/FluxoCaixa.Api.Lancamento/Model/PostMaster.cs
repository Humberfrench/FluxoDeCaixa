namespace FluxoCaixa.Api.Lancamento.Model
{
    public class PostMaster
    {
        public PostMaster()
        {
            Descricao = string.Empty;
            Observacao = string.Empty;
        }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
    }
}
