using FluxoDeCaixa.Domain.Interfaces.Services;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;

namespace FluxoDeCaixa.Service
{
    public class LancamentoService : BaseService<Lancamento>, ILancamentoService
    {
        public LancamentoService(IBaseRepository<Lancamento> repository) : base(repository)
        {
        }
    }
}