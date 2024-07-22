namespace FluxoDeCaixa.Api
{
    using FluxoDeCaixa.Api.Authority;
    using FluxoDeCaixa.Api.Model;
    using ElmahCore.Mvc;
    using ElmahCore.Sql;
    using System.Text.Json.Serialization;
    using System.Text.RegularExpressions;
    using static FluxoDeCaixa.Ioc.Bootstraper;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Elmah = new Elmah();
            var configElmah = Configuration.GetSection("Elmah");
            if (configElmah != null)
            {
                Elmah = configElmah.Get<Elmah>()!;
                if (configElmah != null)
                {
                    Elmah = configElmah.Get<Elmah>()!;
                    Elmah ??= new Elmah
                    {
                        UseElmah = false,
                    };
                }
            }

        }

        public IConfiguration Configuration { get; }
        private Elmah Elmah { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDistributedMemoryCache();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });


            // Add services to the container.
            services.AddJWTTokenServices(Configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                    }
                });
                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();
            });


            services.Configure<IISOptions>(options => options.AutomaticAuthentication = true);


            var config = Configuration.GetSection("ConnectionStrings");
            var connectionString = config.Get<ConnectionStrings>();


            if (Elmah.UseElmah)
            {
                services.AddElmah();
                services.AddElmah<SqlErrorLog>(options =>
                {
                    options.ConnectionString = connectionString.ElmahContext;
                    //options.SqlServerDatabaseSchemaName = "Errors"; //Defaults to dbo if not set
                    //options.SqlServerDatabaseTableName = "ELMAH_Error"; //Defaults to ELMAH_Error if not set
                });
            }
            //container IOC and Contexts
            Initializer(services, Configuration);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger(c => c.SerializeAsV2 = true);

            app.UseSwaggerUI(c =>
            {
                c.InjectStylesheet("/themes/3.x/theme-feeling-blue.css");
                //c.InjectStylesheet("/themes/3.x/theme-flattop.css");
                //c.InjectStylesheet("/themes/3.x/theme-monokai.css");
                //c.InjectStylesheet("/themes/3.x/theme-muted.css");
                //c.InjectStylesheet("/themes/3.x/theme-material.css");
                //c.InjectStylesheet("/themes/3.x/theme-newspaper.css");
                //c.InjectStylesheet("/themes/3.x/theme-outline.css");
                c.EnableTryItOutByDefault();

                c.DocumentTitle = "Fluxo de Caixa";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fluxo de Caixa v3");
                c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
                var aspnetcore_urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? string.Empty;

                var match = Regex.Match(aspnetcore_urls, @"(http:\/\/)([a-zA-Z0-9&_\.*-]{0,100})(:)([0-9]{0,5})");

                if (match.Success)
                {
                    var swaggerBaseRoute = aspnetcore_urls.Substring(match.Length, aspnetcore_urls.Length - match.Length);

                    if (!swaggerBaseRoute.StartsWith("/"))
                        swaggerBaseRoute = $"/{swaggerBaseRoute}";

                    if (!swaggerBaseRoute.EndsWith("/"))
                        swaggerBaseRoute = $"{swaggerBaseRoute}/";

                }
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (Elmah.UseElmah)
            {
                app.UseElmah();
            }

        }


    }
}
