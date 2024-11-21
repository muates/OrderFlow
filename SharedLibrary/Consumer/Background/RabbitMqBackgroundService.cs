using Microsoft.Extensions.Hosting;
using SharedLibrary.Consumer.Abstract;

namespace SharedLibrary.Consumer.Background;

public class RabbitMqBackgroundService(IEnumerable<IRabbitMqConsumer> consumers) : BackgroundService
{
    private readonly IEnumerable<IRabbitMqConsumer> _consumers = consumers ?? throw new ArgumentNullException(nameof(consumers));
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var consumer in _consumers)
        {
            consumer.Start();
        }

        return Task.CompletedTask;
    }
}