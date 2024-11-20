namespace SharedLibrary.Config.Data;

public abstract class RabbitMqExchangeConfigData
{
    public static string? OrderExchange => Environment.GetEnvironmentVariable("RABBITMQ_ORDER_EXCHANGE");
}