using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace SharedLibrary.Extension;

public static class RabbitMqConnectionExtensions
{
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
}