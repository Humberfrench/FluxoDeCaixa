using FluxoCaixa.Domain.Consolidado.Interfaces.Repository;
using FluxoCaixa.Domain.Consolidado.Interfaces.Services;
using FluxoCaixa.Domain.Master.Interfaces;
using FluxoCaixa.Repository.Consolidado;
using FluxoCaixa.Repositoy.Log;
using FluxoCaixa.Service.Lancamentos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Ioc.Consolidado
{
    public static class Bootstraper
    {

        public static void Initializer(IServiceCollection services,
                                       IConfiguration configuration)
        {

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<ILancamentoService, LancamentoService>();
            services.AddScoped<IRepositoryLancamento, RepositoryLancamento>();
            services.AddScoped<IRepositoryLog, RepositoryLog>();

        }
    }
}
