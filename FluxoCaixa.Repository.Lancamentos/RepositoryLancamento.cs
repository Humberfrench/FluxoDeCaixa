using Dietcode.Database;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Repository;
using FluxoCaixa.Domain.Master.Entity;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Repository.Lancamentos
{
    public class RepositoryLancamento : RepositoryBase, IRepositoryLancamento
    {
        private readonly Repository<Lancamento> repository;

        public RepositoryLancamento(IConfiguration config) : base(config)
        {
            repository = new Repository<Lancamento>(connectionString, banco);
        }

        public async Task<int> Add(Lancamento lancamento)
        {
            return await repository.AddAsync(lancamento);
        }
    }
}
