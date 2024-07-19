
using FluxoCaixa.Domain.Lancamentos.Messaging;
using FluxoCaixa.Domain.Lancamentos.ObjectValue;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
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
        private readonly ILancamentoService lancamentoService;
        public LancamentoCreatedSubscriber(ILancamentoService lancamentoService)
        {
            this.lancamentoService = lancamentoService;
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

            consumer.Received += async (sender, eventArgs) =>
            {
                var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var @event = JsonConvert.DeserializeObject<Message>(content);

                await ProcessMessage(@event);
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

        private async Task ProcessMessage(Message message)
        {
            // Process message
            var lancamentos = JsonConvert.DeserializeObject<Lancamentos>(message.Content);
            await lancamentoService.Lancar(lancamentos);
        }
    }
}
