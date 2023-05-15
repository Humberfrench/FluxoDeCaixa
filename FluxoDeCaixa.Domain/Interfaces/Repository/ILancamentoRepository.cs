using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.ObjectValues;

namespace FluxoDeCaixa.Domain.Interfaces.Repository
{
    public interface ILancamentoRepository : IBaseRepository<Lancamento>
    {
        Task<ConsolidadoLancamento> ObterConsolidadoPorData(DateTime data);
        Task<ConsolidadoLancamento> ObterConsolidadoDia();
    }
}
