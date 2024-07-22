using Dietcode.Api.Core.Results;

namespace FluxodeCaixa.ViewModel.Interfaces
{
    public interface ILancamentoApp
    {
        Task<MethodResult> Estornar(int id);
        Task<MethodResult> LancamentosDoDia();
        Task<MethodResult> EstornosDoDia();
        Task<MethodResult> LancamentosPorData(DateTime data);
        Task<MethodResult> LancamentosPorFaixaData(DateTime dataInicial, DateTime dataFinal);
        Task<MethodResult> LancamentosPorMes(int mes, int ano);
        Task<MethodResult> EstornosPorMes(int mes, int ano);
        Task<MethodResult> Lancar(LancamentoViewModel lancamento);
        Task<MethodResult> ObterLancamentosConsolidado();
        Task<MethodResult> ObterLancamentosConsolidadoPorData(DateTime data);
        Task<MethodResult> ObterLancamentosConsolidadoPorDataSql(DateTime data);
        Task<MethodResult> ObterLancamentosConsolidadoSql();
    }
}
