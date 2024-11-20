using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SharedLibrary.Context.Factory;
using SharedLibrary.Model;
using SharedLibrary.Producer.Abstract;
using SharedLibrary.Producer.Concrete;

namespace SharedLibrary.Extension;

public static class RabbitMqServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqServices(this IServiceCollection services)
    {
        services.AddSingleton<IConnection>(provider =>
        {
            var connection = RabbitMqConnectionFactory.CreateConnection();
            return connection;
        });
        
        services.AddSingleton<IMessageSender<Order>, RabbitMqMessageSender<Order>>();

        return services;
    }
}