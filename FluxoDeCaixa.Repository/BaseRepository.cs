using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Repository.Context;
using FluxoDeCaixa.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace FluxoDeCaixa.Repository
{

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> DbSet;
        protected readonly FluxoDeCaixaContext Context;
        protected readonly IContextManager contextManager;
        protected readonly SqlConnection connection;


        public BaseRepository(IContextManager contextManager)
        {
            this.contextManager = contextManager;
            Context = contextManager.GetContext();
            DbSet = Context.Set<TEntity>();
            //try
            //{
            //    var lista = DbSet.ToList();

            //}
            //catch (Exception ex)
            //{
            //    var x = ex.Message;
            //}
            connection = new SqlConnection(contextManager.GetConnectionString);
        }

        //for dapper
        public IDbConnection Connection => connection;

        public async virtual Task<bool> Adicionar(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            await DbSet.AddAsync(entity);
            entry.State = EntityState.Added;
            return true;
        }

        public async virtual Task<bool> AdicionarRange(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);

            return true;
        }

        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }


        public async virtual Task<bool> Atualizar(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            await Task.Run(() => DbSet.Attach(entity));
            entry.State = EntityState.Modified;
            return true;
        }

        public async virtual Task<bool> Remover(TEntity entity)
        {
            var entry = Context.Entry(entity);
            await Task.Run(() => DbSet.Remove(entity));
            //DbSet.Remove(entity);
            entry.State = EntityState.Deleted;

            return true;
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => DbSet.RemoveRange(entities));
        }

        public async virtual Task<TEntity> ObterPorId(int id)
        {
            var resultado = await DbSet.FindAsync(id);
            return resultado ?? Activator.CreateInstance<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> ObterTodos()
        {
            var lista = await DbSet.ToListAsync();
            return lista;
        }

        public async virtual Task<IEnumerable<TEntity>> ObterTodosPaginado(int pagina, int registros)
        {
            return await DbSet.Take(pagina).Skip(registros).ToListAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}