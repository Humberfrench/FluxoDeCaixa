

namespace FluxoDeCaixa.Domain.Entity;

public partial class TipoLancamento
{
    public TipoLancamento()
    {
        Lancamento = new List<Lancamento>();
        Descricao = string.Empty;
    }
    public int TipoLancamentoId { get; set; }

    public string Descricao { get; set; }

    public virtual IList<Lancamento> Lancamento { get; set; }
}