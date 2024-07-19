using Dietcode.Database.Enums;
using FluxoCaixa.Api.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FluxoCaixa.Repository.Consolidado
{
    public class RepositoryBase
    {
        private readonly IConfiguration config;
        protected readonly IDbConnection Connection;
        public RepositoryBase(IConfiguration config)
        {
            Connection = new SqlConnection();

            this.config = config;
            Configuration = new Configuration();

            var configData = config.GetSection("configuration");

            Configuration = configData.Get<Configuration>() ?? new Configuration();

            connectionString = config.GetConnectionString("basic") ?? "";
            Connection.ConnectionString = connectionString;
            Connection.Open();

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

