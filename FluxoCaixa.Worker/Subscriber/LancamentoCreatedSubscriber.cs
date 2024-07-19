
using FluxoCaixa.Domain.Lancamentos.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
namespace FluxoCaixa.Worker.Subscriber
{
    public class LancamentoCreatedSubscriber : IHostedService
    {
        private readonly IModel _channel;
        private const string QUEUE = "Lancamentos";

        public LancamentoCreatedSubscriber()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "201.0.21.4",
                UserName = "humber",
                Password = "French@2908"
            };

            var connection = connectionFactory.CreateConnection("subscriber-lancamentos-connection");

            _channel = connection.CreateModel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var @event = JsonSerializer.Deserialize<Message>(content);

                Console.WriteLine($"Message received: {content}");

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: QUEUE, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
