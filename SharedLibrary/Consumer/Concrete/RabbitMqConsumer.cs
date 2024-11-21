using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Consumer.Abstract;

namespace SharedLibrary.Consumer.Concrete;

public class RabbitMqConsumer<TMessage>(
    IModel channel,
    string queueName,
    string exchangeName,
    IMessageHandler<TMessage> handler)
    : IRabbitMqConsumer
{
    private readonly IModel _channel = channel ?? throw new ArgumentNullException(nameof(channel));
    private readonly string _queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
    private readonly string _exchangeName = exchangeName ?? throw new ArgumentNullException(nameof(exchangeName));
    private readonly IMessageHandler<TMessage> _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public void Start()
    {
        _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(_queueName, _exchangeName, string.Empty);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (sender, e) =>
        {
            var messageJson = Encoding.UTF8.GetString(e.Body.ToArray());
            var message = JsonSerializer.Deserialize<TMessage>(messageJson);
            _handler.HandleMessage(message!);
        };

        _channel.BasicConsume(_queueName, autoAck: true, consumer);
    }
}