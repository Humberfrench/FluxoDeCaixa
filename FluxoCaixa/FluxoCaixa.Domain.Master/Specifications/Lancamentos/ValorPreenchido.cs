using Dietcode.Core.DomainValidator.Interfaces;
using FluxoCaixa.Domain.Master.Entity;

namespace FluxoCaixa.Domain.Master.Specifications.Lancamentos
{
    public class ValorPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => entidade.Valor != 0;

    }
}
