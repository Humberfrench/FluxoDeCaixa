using FluxoDeCaixa.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Repository.Interfaces
{
    public interface IContextManager
    {
        FluxoDeCaixaContext GetContext();
        string GetConnectionString { get; }
    }
}
