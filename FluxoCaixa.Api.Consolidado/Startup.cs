using FluxoCaixa.Api.Auth.Authority;
using FluxoCaixa.Api.Consolidado.Helpers;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static FluxoCaixa.Ioc.Consolidado.Bootstraper;

namespace FluxoCaixa.Api.Consolidado
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.MaxDepth = 256;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });


            // Add services to the container.
            services.AddJWTTokenServices(Configuration);

            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FluxoCaixa Api Consolidado",
                    Version = "v1",
                    Description = $"FluxoCaixa Api Consolidado - Version: {AppVersionService.Version}",
                    Contact = new OpenApiContact
                    {
                        Name = "Suporte",
                        Email = "suporte@dietcode.com.br",
                        Url = new Uri("https://www.dietcode.com.br"),
                    },

                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autorização com token Bearer."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
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

            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.EnableTryItOutByDefault();

                c.DocumentTitle = "FluxoCaixa Api Consolidado";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FluxoCaixa Api Consolidado v1");
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

            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
