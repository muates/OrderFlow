using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SharedLibrary.Consumer.Abstract;
using SharedLibrary.Consumer.Background;
using SharedLibrary.Context.Factory;
using SharedLibrary.Model;
using SharedLibrary.Producer.Abstract;
using SharedLibrary.Producer.Concrete;

namespace SharedLibrary.Extension;

public static class RabbitMqServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqConnection(this IServiceCollection services)
    {
        services.AddSingleton<IConnection>(provider =>
        {
            var connection = RabbitMqConnectionFactory.CreateConnection();
            return connection;
        });

        return services;
    }
    
    public static IServiceCollection AddRabbitMqChannel(this IServiceCollection services)
    {
        services.AddSingleton<IModel>(provider =>
        {
            var connection = provider.GetRequiredService<IConnection>();
            var channel = connection.CreateModel();
            return channel;
        });

        return services;
    }
    
    public static IServiceCollection AddRabbitMqServices(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSender<Order>, RabbitMqMessageSender<Order>>();

        return services;
    }

    public static IServiceCollection AddRabbitMqMessageHandler<TMessage, THandler>(this IServiceCollection services)
        where TMessage : class
        where THandler : class, IMessageHandler<TMessage>
    {
        services.AddSingleton<IMessageHandler<TMessage>, THandler>();
        
        return services;
    }

    public static IServiceCollection AddRabbitMqConsumer<TMessage, TConsumer>(
        this IServiceCollection services, string queueName, string exchangeName)
        where TMessage : class
        where TConsumer : class, IRabbitMqConsumer
    {
        services.AddSingleton<IRabbitMqConsumer>(provider =>
        {
            var channel = provider.GetRequiredService<IModel>();
            var handler = provider.GetRequiredService<IMessageHandler<TMessage>>();
            return ActivatorUtilities.CreateInstance<TConsumer>(provider, channel, queueName, exchangeName, handler);
        });

        return services;
    }
    
    public static IServiceCollection AddRabbitMqHostedService(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqBackgroundService>();
        
        return services;
    }
}