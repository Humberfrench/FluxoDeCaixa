using Dietcode.Core.DomainValidator.Interfaces;
using FluxoCaixa.Domain.Master.Entity;

namespace FluxoCaixa.Domain.Master.Specifications.Lancamentos
{
    public class DataPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => entidade.Data != DateTime.MinValue;

    }
}
