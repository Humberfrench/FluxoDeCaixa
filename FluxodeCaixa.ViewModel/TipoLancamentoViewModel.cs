using System.Text.Json.Serialization;

namespace FluxodeCaixa.ViewModel
{
    public partial class TipoLancamentoViewModel
    {
        public TipoLancamentoViewModel()
        {
            Lancamento = new List<LancamentoViewModel>();
            Descricao = string.Empty;
        }
        public int TipoLancamentoId { get; set; }

        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual IList<LancamentoViewModel> Lancamento { get; set; }
    }
}