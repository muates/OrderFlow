using SharedLibrary.Consumer.Abstract;
using SharedLibrary.Model;

namespace Consumer.StockService.Handler;

public class StockMessageHandler : IMessageHandler<Order>
{
    public void HandleMessage(Order message)
    {
        Console.WriteLine($"Processing stock for order: {message}");
    }
}