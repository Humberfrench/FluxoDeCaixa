using FluxoCaixa.Domain.Master.Entity;

namespace FluxoCaixa.Domain.Lancamentos.Interfaces.Repository
{
    public interface IRepositoryLancamento
    {
        Task<int> Add(Lancamento lancamento);
    }
}
