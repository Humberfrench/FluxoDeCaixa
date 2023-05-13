using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Domain.Interfaces.Services;

namespace FluxoDeCaixa.Service
{
    public class TipoLancamentoService : BaseService<TipoLancamento>, ITipoLancamentoService
    {
        public TipoLancamentoService(IBaseRepository<TipoLancamento> repository) : base(repository)
        {
        }
    }
}