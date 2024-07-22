namespace FluxoCaixa.Domain.Lancamentos.Messaging
{
    public interface ILancamentoService
    {
        Task<bool> Lancar(ObjectValue.Lancamentos lancamentos);
    }
}
