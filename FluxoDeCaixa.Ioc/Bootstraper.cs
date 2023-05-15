using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace FluxoDeCaixa.Ioc
{
    using Domain.Interfaces.Repository;
    using Domain.Interfaces.Services;
    using FluxodeCaixa.ViewModel.Interfaces;
    using FluxoDeCaixa.App;
    using Repository;
    using Repository.Context;
    using Repository.Interfaces;
    using Repository.UnitOfWork;
    using Service;
        
    public static class Bootstraper
    {

        public static void Initializer(IServiceCollection services,
                                       IConfiguration configuration)
        {

            //Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return new ClaimsPrincipal();
            });

            services.AddScoped<IContextManager, ContextManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<FluxoDeCaixaContext>(options => options.UseSqlServer(configuration.GetConnectionString("DietManagerContext")));

            //Others
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

            services.AddScoped<ILancamentoApp, LancamentoApp>();
            services.AddScoped<ILancamentoService, LancamentoService>();
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            //services.AddScoped<ILancamentoService, LancamentoService>();
            //services.AddScoped<ILancamentoService, LancamentoService>();
            //services.AddScoped<ILancamentoService, LancamentoService>();

        }
    }
}