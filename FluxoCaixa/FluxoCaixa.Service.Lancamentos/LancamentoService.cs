using Dietcode.Core.Lib;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Repository;
using FluxoCaixa.Domain.Lancamentos.Messaging;
using FluxoCaixa.Domain.Master.Entity;
using FluxoCaixa.Domain.Master.Interfaces;
using Newtonsoft.Json;
using ObjectValue = FluxoCaixa.Domain.Lancamentos.ObjectValue;

namespace FluxoCaixa.Service.Lancamentos
{
    public class LancamentoService : ILancamentoService
    {
        readonly IRepositoryLancamento repositoryLancamento;
        readonly IRepositoryLog repositoryLog;

        public LancamentoService(IRepositoryLancamento repositoryLancamento, IRepositoryLog repositoryLoo)
        {
            this.repositoryLancamento = repositoryLancamento;
            this.repositoryLog = repositoryLoo;
        }

        public async Task<bool> Lancar(ObjectValue.Lancamentos lancamentos)
        {
            Log log;
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

            if(!lancamento.IsValid())
                {
                log = new Log
                {
                    Data = DateTime.Now,
                    Descricao = "Erro ao validar lançamento",
                    Service = "LancamentoService",
                    Method = "Lancar",
                    Json = lancamento.ToJson(),
                    Erros = lancamento.Erros.ToJson()
                };
                await repositoryLog.Add(log);
                return false;
            }

            var retorno = await repositoryLancamento.Add(lancamento);

            log = new Log
            {
                Data = DateTime.Now,
                Descricao = $"Registered Lancamento. Id {retorno}",
                Service = "LancamentoService",
                Method = "Lancar",
                Json = lancamento.ToJson(),
                Erros = ""
            };

            await repositoryLog.Add(log);

            return retorno != 0;
        }
    }
}
