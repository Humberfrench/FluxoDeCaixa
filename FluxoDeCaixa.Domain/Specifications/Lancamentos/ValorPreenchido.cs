using FluxoDeCaixa.Domain.Entity;
using Dietcode.Core.DomainValidator.Interfaces;

namespace FluxoDeCaixa.Domain.Specifications.Lancamentos
{
    public class ValorPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => entidade.Valor != 0;

    }
}
