using Dietcode.Database;
using FluxoCaixa.Domain.Consolidado.Interfaces.Repository;
using FluxoCaixa.Domain.Master.Entity;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Repository.Consolidado
{
    public class RepositoryLancamento : RepositoryBase, IRepositoryLancamento
    {
        private readonly Repository<Lancamento> repository;

        public RepositoryLancamento(IConfiguration config) : base(config)
        {
            repository = new Repository<Lancamento>(connectionString, banco);
        }
    }
}
