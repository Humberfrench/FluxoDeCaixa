using Dapper;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.Interfaces.Repository;
using FluxoDeCaixa.Domain.ObjectValues;
using FluxoDeCaixa.Repository.Interfaces;
using System.Data;

namespace FluxoDeCaixa.Repository
{
    public class LancamentoRepository : BaseRepository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(IContextManager contextManager) : base(contextManager)
        {

        }

        public async Task<ConsolidadoLancamento> ObterConsolidadoDia()
        {
            var sql = "ObterLancamentosHoje";

            return await connection.QueryFirstOrDefaultAsync<ConsolidadoLancamento>(sql, commandType: CommandType.StoredProcedure);
        }

        public async Task<ConsolidadoLancamento> ObterConsolidadoPorData(DateTime data)
        {
            var sql = "ObterLancamentosPorData";

            var param = new DynamicParameters();
            param.Add("@data", data.ToString("yyyyMMdd"));

            return await connection.QueryFirstOrDefaultAsync<ConsolidadoLancamento>(sql, param: param, commandType: CommandType.StoredProcedure);
        }

    }
}
