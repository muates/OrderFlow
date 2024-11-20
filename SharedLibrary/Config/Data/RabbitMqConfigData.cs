namespace SharedLibrary.Config.Data;

public abstract class RabbitMqConfigData
{
    public static string? Host => Environment.GetEnvironmentVariable("RABBITMQ_HOST");
    public static int Port => int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? string.Empty);
    public static string? Username => Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
    public static string? Password => Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
}