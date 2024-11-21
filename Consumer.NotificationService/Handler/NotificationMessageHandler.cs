using SharedLibrary.Consumer.Abstract;
using SharedLibrary.Model;

namespace Consumer.NotificationService.Handler;

public class NotificationMessageHandler : IMessageHandler<Order>
{
    public void HandleMessage(Order message)
    {
        Console.WriteLine($"Processing notification for order: {message}");
    }
}