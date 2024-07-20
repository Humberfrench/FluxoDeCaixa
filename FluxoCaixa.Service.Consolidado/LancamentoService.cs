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
        public async Task<Domain.Consolidado.ObjectValue.LancamentoConsolidado> ObterLancamentos()
        {
            var lancamentos = await repositoryLancamento.ObterLancamentos();
            return ObterConsolidado(lancamentos.ToList());
        }

        public async Task<Domain.Consolidado.ObjectValue.LancamentoConsolidado> ObterLancamentosDia()
        {
            var lancamentos = await repositoryLancamento.ObterLancamentosDia();
            return ObterConsolidado(lancamentos.ToList());
        }

        public async Task<Domain.Consolidado.ObjectValue.LancamentoConsolidado> ObterLancamentosFaixaDeDataAteHoje(DateTime dataInicial)
        {
            var lancamentos = await repositoryLancamento.ObterLancamentosFaixaDeDataAteHoje(dataInicial);
            return ObterConsolidado(lancamentos.ToList());
        }

        public async Task<Domain.Consolidado.ObjectValue.LancamentoConsolidado> ObterLancamentosFaixaDeDatas(DateTime dataInicial, DateTime dataFinal)
        {
            var lancamentos = await repositoryLancamento.ObterLancamentosFaixaDeDatas(dataInicial, dataFinal);
            return ObterConsolidado(lancamentos.ToList());
        }

        public async Task<Domain.Consolidado.ObjectValue.LancamentoConsolidado> ObterLancamentosMesEspecifico(int month, int year)
        {
            //using link - only to demonstrate. Dapper in this case is more efficient
            var lancamentos = await repositoryLancamento.ObterLancamentos();
            var lancamentosRetorno = lancamentos.Where(x => x.Data.Month == month && x.Data.Year == year).ToList();
            return ObterConsolidado(lancamentosRetorno);
        }

        private Domain.Consolidado.ObjectValue.LancamentoConsolidado ObterConsolidado(List<Domain.Consolidado.ObjectValue.Lancamentos> lancamentos)
        {
            var consolidado = new Domain.Consolidado.ObjectValue.LancamentoConsolidado();
            consolidado.Lancamentos = lancamentos;
            consolidado.Debitos = lancamentos.Where(x => x.TipoLancamentoId == 2).Sum(x => x.Valor);
            consolidado.Creditos = lancamentos.Where(x => x.TipoLancamentoId == 1).Sum(x => x.Valor);
            consolidado.Saldo = consolidado.Creditos + (consolidado.Debitos * -1);
            return consolidado;
        }
    }
}
