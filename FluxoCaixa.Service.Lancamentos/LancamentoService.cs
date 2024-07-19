using FluxoCaixa.Domain.Lancamentos.Interfaces.Repository;
using FluxoCaixa.Domain.Lancamentos.Messaging;
using FluxoCaixa.Domain.Master.Entity;
using ObjectValue = FluxoCaixa.Domain.Lancamentos.ObjectValue;

namespace FluxoCaixa.Service.Lancamentos
{
    public class LancamentoService : ILancamentoService
    {
        readonly IRepositoryLancamento repositoryLancamento;

        public LancamentoService(IRepositoryLancamento repositoryLancamento)
        {
            this.repositoryLancamento = repositoryLancamento;
        }

        public async Task<bool> Lancar(ObjectValue.Lancamentos lancamentos)
        {
            var lancamento = new Lancamento
            {
                LancamentoId = lancamentos.LancamentoId,
                Data = lancamentos.Data,
                Valor = lancamentos.Valor,
                TipoLancamentoId = lancamentos.TipoLancamentoId,
                Descricao = lancamentos.Descricao,
                Observacao = lancamentos.Observacao,
                Estornado = lancamentos.Estornado
            };
            var retorno = await repositoryLancamento.Add(lancamento);

            return true;
        }
    }
}
