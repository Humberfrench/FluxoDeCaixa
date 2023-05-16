using FluxoDeCaixa.Domain.Entity;
using Dietcode.Core.DomainValidator.Interfaces;

namespace FluxoDeCaixa.Domain.Specifications.Lancamentos
{
    public class TipoLancamentoPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => entidade.TipoLancamentoId != 0;

    }
}
