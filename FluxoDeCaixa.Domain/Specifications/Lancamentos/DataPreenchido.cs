using FluxoDeCaixa.Domain.Entity;
using Dietcode.Core.DomainValidator.Interfaces;

namespace FluxoDeCaixa.Domain.Specifications.Lancamentos
{
    public class DataPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => entidade.Data != DateTime.MinValue;

    }
}
