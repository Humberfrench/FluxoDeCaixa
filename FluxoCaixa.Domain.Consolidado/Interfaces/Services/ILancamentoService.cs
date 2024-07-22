using FluxoCaixa.Domain.Consolidado.ObjectValue;

namespace FluxoCaixa.Domain.Consolidado.Interfaces.Services
{
    public interface ILancamentoService
    {
        Task<LancamentoConsolidado> ObterLancamentos();
        Task<LancamentoConsolidado> ObterLancamentosDia();
        Task<LancamentoConsolidado> ObterLancamentosFaixaDeDatas(DateTime dataInicial, DateTime dataFinal);
        Task<LancamentoConsolidado> ObterLancamentosFaixaDeDataAteHoje(DateTime dataInicial);
        Task<LancamentoConsolidado> ObterLancamentosMesEspecifico(int month, int year);

    }
}
