using Dapper;
using Dietcode.Database;
using FluxoCaixa.Domain.Consolidado.Interfaces.Repository;
using FluxoCaixa.Domain.Consolidado.ObjectValue;
using Microsoft.Extensions.Configuration;
using static FluxoCaixa.Repository.Consolidado.Scripts.RepositoryLancamentoScripts;

namespace FluxoCaixa.Repository.Consolidado
{
    public class RepositoryLancamento : RepositoryBase, IRepositoryLancamento
    {

        public RepositoryLancamento(IConfiguration config) : base(config)
        {

        }
        public async Task<IList<Lancamentos>> ObterLancamentos()
        {
            var sql = ObterLancamentosScript();
            var result = await Connection.QueryAsync<Lancamentos>(sql);
            return result.ToList();
        }

        public async Task<IList<Lancamentos>> ObterLancamentosDia()
        {
            var sql = ObterLancamentosDiaScript();
            var result = await Connection.QueryAsync<Lancamentos>(sql);
            return result.ToList();
        }

        public async Task<IList<Lancamentos>> ObterLancamentosFaixaDeDatas(DateTime dataInicial, DateTime dataFinal)
        {
            var sql = ObterLancamentosFaixaDeDatasScript(dataInicial, dataFinal);
            var result = await Connection.QueryAsync<Lancamentos>(sql);
            return result.ToList();
        }

        public async Task<IList<Lancamentos>> ObterLancamentosFaixaDeDataAteHoje(DateTime dataInicial)
        {
            var hoje = DateTime.Now;
            var sql = ObterLancamentosFaixaDeDatasScript(dataInicial,hoje);
            var result = await Connection.QueryAsync<Lancamentos>(sql);
            return result.ToList();
        }

        public async Task<IList<Lancamentos>> ObterLancamentosMesEspecifico(int month, int year)
        {
            var sql = ObterLancamentosMesEspecificoScript(month, year);
            var result = await Connection.QueryAsync<Lancamentos>(sql);
            return result.ToList();
        }
    }
}
