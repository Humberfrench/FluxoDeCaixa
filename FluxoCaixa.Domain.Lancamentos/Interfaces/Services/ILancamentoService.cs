namespace FluxoCaixa.Domain.Lancamentos.Interfaces.Services
{
    public interface ILancamentoService
    {
        Task<bool> Lancar(ObjectValue.Lancamentos lancamentos);
    }
}
