using static FluxoCaixa.Ioc.Lancamento.Bootstraper;
using FluxoCaixa.Worker.Subscriber;
using Microsoft.OpenApi.Models;
using FluxoCaixa.Worker.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Worker",
        Version = "v1",
        Description = $"Worker - Version: {AppVersionService.Version}",
        Contact = new OpenApiContact
        {
            Name = "Suporte",
            Email = "suporte@dietcode.com.br",
            Url = new Uri("https://www.dietcode.com.br"),
        },

    });
});

//IConfiguration configuration = builder.Configuration;

builder.Services.AddHostedService<LancamentoCreatedSubscriber>();
InitializerWorker(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnableTryItOutByDefault();

        c.DocumentTitle = "Worker";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Worker v1");
        c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
