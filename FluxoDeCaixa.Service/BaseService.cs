using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace FluxoDeCaixa.Service
{
    public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async virtual Task<bool> Adicionar(TEntity obj)
        {
            return await repository.Adicionar(obj);
        }

        public async virtual Task<bool> Atualizar(TEntity obj)
        {
            return await repository.Atualizar(obj);
        }

        public async virtual Task<bool> Remover(TEntity obj)
        {
            return await repository.Remover(obj);
        }
        public async virtual Task<bool> Remover(int id)
        {
            var obj = await ObterPorId(id);
            if (obj == null)
            {
                return false;
            }
            return await repository.Remover(obj);
        }

        public async virtual Task<TEntity> ObterPorId(int id)
        {
            return await repository.ObterPorId(id);
        }

        public async virtual Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await repository.ObterTodos();
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public async virtual Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate)
        {
            return await repository.Pesquisar(predicate);
        }
    }
}
