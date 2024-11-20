using RabbitMQ.Client;
using SharedLibrary.Config.Data;

namespace SharedLibrary.Context.Factory;

public static class RabbitMqConnectionFactory
{
    public static IConnection CreateConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMqConfigData.Host,
            Port = RabbitMqConfigData.Port,
            UserName = RabbitMqConfigData.Username,
            Password = RabbitMqConfigData.Password
        };

        return factory.CreateConnection();
    }
}