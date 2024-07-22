using Dietcode.Database.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Domain.Master.Entity
{
    public abstract class BaseEntity
    {
        protected bool isValid;

        protected BaseEntity()
        {
            isValid = false;
            Erros = new List<string>();
        }
        [WriteCol(false)]
        public virtual List<string> Erros { get; }

        public abstract bool IsValid();
    }
}
