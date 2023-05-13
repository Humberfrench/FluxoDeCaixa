using Dapper;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Repository.Interfaces;

namespace FluxoDeCaixa.Repository
{
    public class LancamentoRepository : BaseRepository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(IContextManager contextManager) : base(contextManager)
        {

        }

    }
}
