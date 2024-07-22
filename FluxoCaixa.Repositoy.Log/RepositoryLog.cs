using Dietcode.Database;
using FluxoCaixa.Domain.Master;
using FluxoCaixa.Domain.Master.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Repositoy.Log
{
    public class RepositoryLog : RepositoryBase, IRepositoryLog
    {
        private readonly Repository<Domain.Master.Entity.Log> repository;

        public RepositoryLog(IConfiguration config) : base(config)
        {
            repository = new Repository<Domain.Master.Entity.Log>(connectionString, banco);
        }

        public async Task<int> Add(Domain.Master.Entity.Log log)
        {
            return await repository.AddAsync(log);
        }
    }
}
