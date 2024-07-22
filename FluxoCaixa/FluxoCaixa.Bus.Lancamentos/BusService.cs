using FluxoCaixa.Domain.Lancamentos.Interfaces.Bus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FluxoCaixa.Bus.Lancamentos
{
    public class BusService : IBusService
    {
        private const string EXCHANGE = "Lancamentos-created";
        private const string QUEUE = "Lancamentos";
        private readonly IModel _channel;

        public BusService()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "201.0.21.4",
                UserName = "humber",
                Password = "French@2908"
            };

            var connection = connectionFactory.CreateConnection("publisher-lancamentos-connection");

            _channel = connection.CreateModel();
            _channel.ExchangeDeclare(exchange: EXCHANGE, type: "topic", durable: true, autoDelete: false);
            _channel.QueueDeclare(queue: QUEUE, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: QUEUE, exchange: EXCHANGE, routingKey: QUEUE);
        }

        public Task Publish<T>(string routingKey, T message)
        {
            var byteArray = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            _channel.BasicPublish(exchange: EXCHANGE, routingKey: routingKey, basicProperties: null, body: byteArray);

            return Task.CompletedTask;
        }
    }
}
