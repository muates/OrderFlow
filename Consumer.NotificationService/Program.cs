using Consumer.NotificationService.Handler;
using SharedLibrary.Config;
using SharedLibrary.Config.Data;
using SharedLibrary.Consumer.Concrete;
using SharedLibrary.Extension;
using SharedLibrary.Model;

var builder = WebApplication.CreateBuilder(args);

// Env load
EnvironmentConfig.LoadEnv();

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddRabbitMqConnection()
    .AddRabbitMqChannel()
    .AddRabbitMqServices()
    .AddRabbitMqMessageHandler<Order, NotificationMessageHandler>()
    .AddRabbitMqConsumer<Order, RabbitMqConsumer<Order>>(
        queueName: RabbitMqConsumerConfigData.NotificationQueueName ?? string.Empty,
        exchangeName: RabbitMqExchangeConfigData.OrderExchange ?? string.Empty)
    .AddRabbitMqHostedService();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consumer Notification Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();