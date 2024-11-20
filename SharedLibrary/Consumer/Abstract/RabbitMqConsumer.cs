using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SharedLibrary.Consumer.Abstract;

public abstract class RabbitMqConsumer<TMessage>(
    IModel channel,
    string? queueName,
    string? exchangeName,
    ILogger<RabbitMqConsumer<TMessage>> logger)
{
    private readonly IModel _channel = channel ?? throw new ArgumentNullException(nameof(channel));
    private readonly ILogger<RabbitMqConsumer<TMessage>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly string _queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
    private readonly string _exchangeName = exchangeName ?? throw new ArgumentNullException(nameof(exchangeName));

    public void Start()
    {
        _channel.QueueDeclare(_queueName, true, false, false);
        _channel.QueueBind(_queueName, _exchangeName, "");
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnMessageReceived;

        _channel.BasicConsume(_queueName, true, consumer);
        _logger.LogInformation($"{typeof(TMessage).Name} consumer is listening for messages.");
    }
    
    private void OnMessageReceived(object? sender, BasicDeliverEventArgs e)
    {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());
        ProcessMessage(message);
    }
    
    protected abstract void ProcessMessage(string message);
}