using Dapper;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Repository.Interfaces;

namespace FluxoDeCaixa.Repository
{
    public class TipoLancamentoRepository : BaseRepository<TipoLancamento>, ITipoLancamentoRepository
    {
        public TipoLancamentoRepository(IContextManager contextManager) : base(contextManager)
        {

        }

    }
}
