using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using SharedLibrary.Helper;
using SharedLibrary.Producer.Abstract;

namespace SharedLibrary.Producer.Concrete;

public class RabbitMqMessageSender<TMessage>(
    IModel channel,
    ILogger<RabbitMqMessageSender<TMessage>> logger)
    : IMessageSender<TMessage>
    where TMessage : class
{
    private readonly IModel _channel = channel ?? throw new ArgumentNullException(nameof(channel));
    private readonly ILogger<RabbitMqMessageSender<TMessage>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task SendMessageAsync(TMessage message, string routingKey, string exchange, string exchangeType = "direct")
    {
        ValidationHelper.ValidateNotNullOrEmpty(exchange, nameof(exchange));
        ValidationHelper.ValidateNotNullOrEmpty(exchangeType, nameof(exchangeType));

        try
        {
            var messageBody = JsonSerializerHelper.Serialize(message);

            _channel.ExchangeDeclare(exchange: exchange, type: exchangeType, durable: true);

            _channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: messageBody
            );

            _logger.LogInformation($"Message sent to exchange {exchange} with routing key {routingKey}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while sending message to RabbitMQ.");
            throw;
        }

        await Task.CompletedTask;
    }
}