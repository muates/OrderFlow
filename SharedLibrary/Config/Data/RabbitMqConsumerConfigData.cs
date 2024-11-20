namespace SharedLibrary.Config.Data;

public abstract class RabbitMqConsumerConfigData
{
    public static string? PaymentQueueName => Environment.GetEnvironmentVariable("RABBITMQ_PAYMENT_QUEUE");
    public static string? StockQueueName => Environment.GetEnvironmentVariable("RABBITMQ_STOCK_QUEUE");
    public static string? NotificationQueueName => Environment.GetEnvironmentVariable("RABBITMQ_NOTIFICATION_QUEUE");
}