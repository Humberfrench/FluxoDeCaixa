using Dietcode.Database.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Domain.Master.Entity
{
    [TableName("Log")]
    public class Log
    {
        public Log()
        {
            Descricao = string.Empty;
            Data = DateTime.Now;
            Service = string.Empty;
            Method = string.Empty;
            Json = string.Empty;
            Erros = string.Empty;
        }

        [KeyId]
        public int LogId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Service { get; set; }
        public string Method { get; set; }
        public string Json { get; set; }
        public string Erros { get; set; }
    }
}
