
namespace FluxoDeCaixa.Domain.Entity;

public partial class Lancamento
{
    public Lancamento()
    {
        Descricao = string.Empty;
        Observacao = string.Empty;
        TipoLancamento = new TipoLancamento();
    }

    public int LancamentoId { get; set; }

    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public int TipoLancamentoId { get; set; }

    public string Descricao { get; set; }

    public string Observacao { get; set; }

    public bool Estornado { get; set; }

    public virtual TipoLancamento TipoLancamento { get; set; }
}