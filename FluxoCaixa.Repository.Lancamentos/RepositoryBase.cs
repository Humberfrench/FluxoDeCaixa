using Dietcode.Database.Enums;
using FluxoCaixa.Api.Config;
using Microsoft.Extensions.Configuration;

namespace FluxoCaixa.Repository.Lancamentos
{
    public class RepositoryBase
    {
        private readonly IConfiguration config;

        public RepositoryBase(IConfiguration config)
        {
            this.config = config;
            Configuration = new Configuration();

            var configData = config.GetSection("configuration");

            Configuration = configData.Get<Configuration>() ?? new Configuration();

            connectionString = config.GetConnectionString("basic") ?? "";

            banco = GetBanco(Configuration.DataBase);
        }

        protected Configuration Configuration { get; }
        protected string connectionString;
        protected EnumBancos banco;

        private EnumBancos GetBanco(string banco)
        {

            switch (banco.ToLower())
            {
                case "sqlserver":
                    return EnumBancos.SqlServer;
                case "mysql":
                    return EnumBancos.MySql;
                case "postgresql":
                    return EnumBancos.PostgreSql;
                case "oracle":
                    return EnumBancos.Oracle;
                default:
                    return EnumBancos.SqlServer;
            }
        }
    }
}

