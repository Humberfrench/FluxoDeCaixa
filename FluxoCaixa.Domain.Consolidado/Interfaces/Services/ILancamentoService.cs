using FluxoCaixa.Domain.Consolidado.ObjectValue;

namespace FluxoCaixa.Domain.Consolidado.Interfaces.Services
{
    public interface ILancamentoService
    {
        Task<IList<Lancamentos>> ObterLancamentos();
        Task<IList<Lancamentos>> ObterLancamentosDia();
        Task<IList<Lancamentos>> ObterLancamentosFaixaDeDatas(DateTime dataInicial, DateTime dataFinal);
        Task<IList<Lancamentos>> ObterLancamentosFaixaDeDataAteHoje(DateTime dataInicial);
        Task<IList<Lancamentos>> ObterLancamentosMesEspecifico(int month, int year);

    }
}
