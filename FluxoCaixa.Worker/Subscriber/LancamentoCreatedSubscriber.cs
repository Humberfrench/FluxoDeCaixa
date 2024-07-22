
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
        private readonly IModel channel;
        private readonly IServiceProvider serviceProvider;
        private ILancamentoService lancamentoService;
        private const string QUEUE = "Lancamentos";
        readonly IRepositoryLog repositoryLog;

        private readonly IList<Message> messages;
        System.Timers.Timer timer;

        public LancamentoCreatedSubscriber(IServiceProvider serviceProvider,
                                           IRepositoryLog repositoryLog)
        {
            //this.lancamentoService = lancamentoService;
            this.serviceProvider = serviceProvider;
            this.repositoryLog = repositoryLog;

            messages = new List<Message>();
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
            var connectionFactory = new ConnectionFactory
            {
                HostName = "201.0.21.4",
                UserName = "humber",
                Password = "French@2908"
            };

            var connection = connectionFactory.CreateConnection("subscriber-lancamentos-connection");

            channel = connection.CreateModel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var @event = JsonConvert.DeserializeObject<Message>(content);
                if (@event != null)
                {
                    messages.Add(@event);
                    //await ProcessMessage(@event);
                    channel.BasicAck(eventArgs.DeliveryTag, false);
                }
            };

            channel.BasicConsume(queue: QUEUE, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (messages.Count == 0)
            {
                return;
            }
            timer.Enabled = false;
            timer.Stop();

            if (messages.Count == 1)
            {
                await ProcessMessage(messages[0]);
                messages.Clear();
            }
            else if (messages.Count > 1)
            {
                await ProcessMessage(messages);
                messages.Clear();
            }

            timer.Enabled = true;
            timer.Start();

        }

        private async Task ProcessMessage(Message message)
        {
            if (message.Content.IsNullOrEmptyOrWhiteSpace())
            {
                return;
            }

            lancamentoService = serviceProvider.GetService<ILancamentoService>();

            // Process message
            var lancamentos = JsonConvert.DeserializeObject<Lancamentos>(message.Content);

            if (lancamentos != null)
            {
                await lancamentoService.Lancar(lancamentos);
            }

        }

        private async Task ProcessMessage(IList<Message> messages)
        {
            var lancamentos = new List<Lancamentos>();
            if (messages.Count == 0)
            {
                return;
            }

            lancamentoService = serviceProvider.GetService<ILancamentoService>();

            foreach (var message in messages)
            {
                if (message.Content.IsNullOrEmptyOrWhiteSpace())
                {
                    continue;
                }

                // Process message
                var lancamento = JsonConvert.DeserializeObject<Lancamentos>(message.Content);

                lancamentos.Add(lancamento);
            }
            // Process message

            if (lancamentos.Count > 0)
            {
                await lancamentoService.Lancar(lancamentos);
            }

        }

    }
}
