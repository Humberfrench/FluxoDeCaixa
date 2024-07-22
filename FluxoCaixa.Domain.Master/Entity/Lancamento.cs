
using Dietcode.Core.Lib;
using Dietcode.Database.Attribute;

namespace FluxoCaixa.Domain.Master.Entity;

[TableName("Lancamento")]
public partial class Lancamento : BaseEntity
{

    public Lancamento()
    {
        Descricao = string.Empty;
        Observacao = string.Empty;
        TipoLancamento = new TipoLancamento();
    }

    [KeyId]
    public int LancamentoId { get; set; }

    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public int TipoLancamentoId { get; set; }

    public string Descricao { get; set; }

    public string Observacao { get; set; }

    public bool Estornado { get; set; }

    [WriteCol(false)]
    public virtual TipoLancamento TipoLancamento { get; set; }


    public override bool IsValid()
    {
        isValid = false;
        if (Descricao.IsNullOrEmptyOrWhiteSpace())
        {
            Erros.Add("Descrição não informada");
        }
        if (Data == DateTime.MinValue)
        {
            Erros.Add("Data não informada");

        }
        if (Valor == 0)
        {
            Erros.Add("Valor não informado");
        }
        if (TipoLancamentoId == 0)
        {
            Erros.Add("Tipo de lançamento não informado");
        }
        isValid = Erros.Count == 0;

        return isValid;

    }

}