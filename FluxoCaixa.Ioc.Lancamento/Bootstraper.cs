using FluxoCaixa.Bus.Lancamentos;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Bus;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Repository;
using FluxoCaixa.Domain.Lancamentos.Messaging;
using FluxoCaixa.Repository.Lancamentos;
using FluxoCaixa.Service.Lancamentos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluxoCaixa.Domain.Master.Interfaces;
using FluxoCaixa.Repositoy.Log;

namespace FluxoCaixa.Ioc.Lancamento
{
    public static class Bootstraper
    {

        public static void Initializer(IServiceCollection services,
                                       IConfiguration configuration)
        {

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<IBusService, BusService>();
            services.AddScoped<ILancamentoService, LancamentoService>();
            services.AddScoped<IRepositoryLancamento, RepositoryLancamento>();
            services.AddScoped<IRepositoryLog, RepositoryLog>();
        }
        public static void Initializer(IServiceCollection services)
        {

            services.AddSingleton<ILancamentoService, LancamentoService>();
            services.AddSingleton<IRepositoryLancamento, RepositoryLancamento>();
            services.AddSingleton<IRepositoryLog, RepositoryLog>();

        }

    }
}
