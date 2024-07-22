using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Repository.Consolidado.Scripts
{
    public static class RepositoryLancamentoScripts
    {
        private static string format = "yyyy-MM-dd";

        public static string ObterLancamentosScript()
        {
            var sql = $@"SELECT	* 
                         FROM	Lancamento";

            return sql;
        }
        public static string ObterLancamentosDiaScript()
        {
            var hoje = DateTime.Now.ToString(format);
            var sql = $@"SELECT	* 
                         FROM	Lancamento
                         WHERE	Data = '{hoje}'";

            return sql;
        }
        public static string ObterLancamentosFaixaDeDatasScript(DateTime dataInicial, DateTime dataFinal)
        {
            var sql = $@"SELECT	* 
                         FROM	Lancamento
                         WHERE	Data >= '{dataInicial.ToString(format)}'
                         AND    Data <= '{dataFinal}'";

            return sql;
        }

        public static string ObterLancamentosMesEspecificoScript(int month, int year)
        {
            var hoje = DateTime.Now.ToString(format);
            var sql = $@"SELECT	* 
                         FROM	Lancamento
                         WHERE	Month(Data) = {month}
                         AND    Year(Data) = {year}";

            return sql;
        }


    }
}
