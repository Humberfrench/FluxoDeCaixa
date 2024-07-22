namespace FluxoCaixa.Domain.Lancamentos.Interfaces.Bus
{
    public interface IBusService
    {
        Task Publish<T>(string routingKey, T message);
    }
}
