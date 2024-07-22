using FluxoDeCaixa.Domain.Entity;
using Dietcode.Core.DomainValidator.Interfaces;
using Dietcode.Core.Lib;

namespace FluxoDeCaixa.Domain.Specifications.Lancamentos
{
    public class DescricaoPreenchido : ISpecification<Lancamento>
    {
        public bool IsSatisfiedBy(Lancamento entidade) => !entidade.Descricao.IsNullOrEmptyOrWhiteSpace();

    }
}
