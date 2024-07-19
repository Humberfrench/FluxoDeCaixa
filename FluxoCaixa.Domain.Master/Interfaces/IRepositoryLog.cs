using FluxoCaixa.Domain.Master.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Domain.Master.Interfaces
{
    public interface IRepositoryLog
    {
        Task<int> Add(Log log);
    }
}
