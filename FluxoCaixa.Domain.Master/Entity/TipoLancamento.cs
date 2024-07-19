using Dietcode.Core.Lib;
using Dietcode.Database.Attribute;

namespace FluxoCaixa.Domain.Master.Entity;

public partial class TipoLancamento : BaseEntity
{
    public TipoLancamento()
    {
        Lancamento = new List<Lancamento>();
        Descricao = string.Empty;
    }
    public int TipoLancamentoId { get; set; }

    public string Descricao { get; set; }

    public virtual IList<Lancamento> Lancamento { get; set; }

    public override bool IsValid()
    {
        isValid = false;
        if (Descricao.IsNullOrEmptyOrWhiteSpace())
        {
            Erros.Add("Descrição não informada");
        }
        isValid = Erros.Count == 0;

        return isValid;
    }
}