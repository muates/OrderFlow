using ProducerService.Extension;
using SharedLibrary.Config;
using SharedLibrary.Extension;

var builder = WebApplication.CreateBuilder(args);

// Env load
EnvironmentConfig.LoadEnv();

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddRabbitMqServices()
    .AddRabbitMqChannel()
    .AddProducerServices();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Producer Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();