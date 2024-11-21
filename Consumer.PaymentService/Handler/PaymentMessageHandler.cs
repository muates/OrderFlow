using SharedLibrary.Consumer.Abstract;
using SharedLibrary.Model;

namespace Consumer.PaymentService.Handler;

public class PaymentMessageHandler : IMessageHandler<Order>
{
    public void HandleMessage(Order message)
    {
        Console.WriteLine($"Processing payment for order: {message}");
    }
}
