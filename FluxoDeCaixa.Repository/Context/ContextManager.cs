using System;
using System.Collections.Generic;
using System.Linq;
using FluxoDeCaixa.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FluxoDeCaixa.Repository.Context
{
    public class ContextManager : IContextManager
    {
        private const string CONTEXT_KEY = "ContextManager.Context";
        private readonly IHttpContextAccessor context;
        private readonly IConfiguration configuration;

        public ContextManager(IHttpContextAccessor context,
                              IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public string GetConnectionString => configuration.GetConnectionString("FluxoDeCaixaContext");

        public FluxoDeCaixaContext GetContext()
        {
            if (context.HttpContext == null)
                return new FluxoDeCaixaContext();

            if (context.HttpContext.Items[CONTEXT_KEY] == null)
            {
                context.HttpContext.Items[CONTEXT_KEY] = new FluxoDeCaixaContext();
            }

            return context.HttpContext.Items[CONTEXT_KEY] as FluxoDeCaixaContext ?? new FluxoDeCaixaContext();
        }
    }
}
