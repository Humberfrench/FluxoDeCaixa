using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Ioc.Consolidado
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

            //    //services.AddScoped<ILancamentoService, LancamentoService>();

        }
    }
}
