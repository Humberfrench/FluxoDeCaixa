
using FluxoCaixa.Domain.Lancamentos.Messaging;
using FluxoCaixa.Domain.Lancamentos.ObjectValue;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using FluxoCaixa.Domain.Master.Entity;
using FluxoCaixa.Domain.Master.Interfaces;
using Dietcode.Core.Lib;
namespace FluxoCaixa.Worker.Subscriber
{
    public class LancamentoCreatedSubscriber : IHostedService
    {
        private readonly IModel _channel;
        private const string QUEUE = "Lancamentos";
        private readonly ILancamentoService lancamentoService;
        readonly IRepositoryLog repositoryLog;
        public LancamentoCreatedSubscriber(ILancamentoService lancamentoService,IRepositoryLog repositoryLog)
        {
            this.lancamentoService = lancamentoService;
            this.repositoryLog = repositoryLog;
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
            var log = new Log
            {
                Data = DateTime.Now,
                Descricao = "Processando Mensagem",
                Service = "Worker",
                Method = "ProcessMessage",
                Json = message.ToJson(),
                Erros = ""
            };
            await repositoryLog.Add(log);

            // Process message
            var lancamentos = JsonConvert.DeserializeObject<Lancamentos>(message.Content);

            await lancamentoService.Lancar(lancamentos);
        }
    }
}
