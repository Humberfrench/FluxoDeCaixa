using Dietcode.Core.DomainValidator.Interfaces;
using Dietcode.Core.Lib;
using FluxoCaixa.Domain.Master.Entity;

namespace FluxoCaixa.Domain.Master.Specifications.Lancamentos
{
    public class DescricaoPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => !entidade.Descricao.IsNullOrEmptyOrWhiteSpace();

    }
}
