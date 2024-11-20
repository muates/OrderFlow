using ProducerService.Service.Abstract;
using RabbitMQ.Client;
using SharedLibrary.Config.Data;
using SharedLibrary.Model;
using SharedLibrary.Producer.Abstract;

namespace ProducerService.Service.Concrete;

public class OrderService(IMessageSender<Order> messageSender) : IOrderService
{
    private readonly IMessageSender<Order> _messageSender =
        messageSender ?? throw new ArgumentNullException(nameof(messageSender));

    public async Task CreateOrderAsync(Order order)
    {
        if (RabbitMqExchangeConfigData.OrderExchange != null)
        {
            await _messageSender.SendMessageAsync(
                message: order,
                routingKey: string.Empty,
                exchange: RabbitMqExchangeConfigData.OrderExchange,
                exchangeType: ExchangeType.Fanout
            );
            
            Console.WriteLine("Order sent to RabbitMQ.");    
        }
    }
}
