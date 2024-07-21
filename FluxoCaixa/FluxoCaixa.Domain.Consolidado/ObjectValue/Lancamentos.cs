namespace FluxoCaixa.Domain.Consolidado.ObjectValue
{
    public class Lancamentos
    {
        public Lancamentos()
        {

            Descricao = string.Empty;
            Observacao = string.Empty;
        }

        public int LancamentoId { get; set; }

        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public int TipoLancamentoId { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public bool Estornado { get; set; }
    }
}
