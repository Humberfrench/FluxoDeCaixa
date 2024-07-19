using FluxoCaixa.Domain.Consolidado.Interfaces.Services;
using FluxoCaixa.Domain.Consolidado;
using FluxoCaixa.Domain.Consolidado.Interfaces.Repository;

namespace FluxoCaixa.Service.Lancamentos
{
    public class LancamentoService : ILancamentoService
    {
        private readonly IRepositoryLancamento repositoryLancamento;
        public LancamentoService(IRepositoryLancamento repositoryLancamento)
        {
            this.repositoryLancamento = repositoryLancamento;
        }
        public async Task<IList<Domain.Consolidado.ObjectValue.Lancamentos>> ObterLancamentos()
        {
            return await repositoryLancamento.ObterLancamentos();
        }

        public async Task<IList<Domain.Consolidado.ObjectValue.Lancamentos>> ObterLancamentosDia()
        {
            return await repositoryLancamento.ObterLancamentosDia();
        }

        public async Task<IList<Domain.Consolidado.ObjectValue.Lancamentos>> ObterLancamentosFaixaDeDataAteHoje(DateTime dataInicial)
        {
            return await repositoryLancamento.ObterLancamentosFaixaDeDataAteHoje(dataInicial);
        }

        public async Task<IList<Domain.Consolidado.ObjectValue.Lancamentos>> ObterLancamentosFaixaDeDatas(DateTime dataInicial, DateTime dataFinal)
        {
            return await repositoryLancamento.ObterLancamentosFaixaDeDatas(dataInicial,dataFinal);
        }

        public async Task<IList<Domain.Consolidado.ObjectValue.Lancamentos>> ObterLancamentosMesEspecifico(int month, int year)
        {
            //using link - only to demonstrate. Dapper in this case is more efficient
            var lancamentos = await repositoryLancamento.ObterLancamentos();
            return lancamentos.Where(x => x.Data.Month == month && x.Data.Year == year).ToList();
        }
    }
}
