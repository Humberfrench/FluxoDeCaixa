using System;
using Dietcode.Core.DomainValidator;
using FluxoDeCaixa.Domain.Entity;
using FluxoDeCaixa.Domain.ObjectValues;

namespace FluxoDeCaixa.Domain.Interfaces.Services
{
    public interface ILancamentoService : IBaseService<Lancamento>
    {
        Task<ValidationResult> Estornar(int id);
        Task<ValidationResult<List<Lancamento>>> LancamentosDoDia();
        Task<ValidationResult<List<Lancamento>>> EstornosDoDia();
        Task<ValidationResult<List<Lancamento>>> LancamentosPorData(DateTime data);
        Task<ValidationResult<List<Lancamento>>> LancamentosPorFaixaData(DateTime dataInicial, DateTime dataFinal);
        Task<ValidationResult<List<Lancamento>>> LancamentosPorMes(int mes, int ano);
        Task<ValidationResult<List<Lancamento>>> EstornosPorMes(int mes, int ano);
        Task<ValidationResult> Lancar(Lancamento lancamento);
        Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidado();
        Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidadoPorData(DateTime data);
        Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidadoPorDataSql(DateTime data);
        Task<ValidationResult<ConsolidadoLancamento>> ObterLancamentosConsolidadoSql();
    }
}
