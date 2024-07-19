using FluxoCaixa.Bus.Lancamentos;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Bus;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Repository;
using FluxoCaixa.Domain.Lancamentos.Interfaces.Services;
using FluxoCaixa.Repository.Lancamentos;
using FluxoCaixa.Service.Lancamentos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Ioc.Lancamento
{
    public static class Bootstraper
    {

        public static void Initializer(IServiceCollection services,
                                       IConfiguration configuration)
        {

            //    //Services
            //    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //    services.AddScoped(provider =>
            //    {
            //        var context = provider.GetService<IHttpContextAccessor>();
            //        return new ClaimsPrincipal();
            //    });

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<IBusService, BusService>();
            services.AddScoped<ILancamentoService, LancamentoService>();
            services.AddScoped<IRepositoryLancamento, RepositoryLancamento>();

        }
    }
}
