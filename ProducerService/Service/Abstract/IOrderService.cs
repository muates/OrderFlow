using SharedLibrary.Model;

namespace ProducerService.Service.Abstract;

public interface IOrderService
{
    Task CreateOrderAsync(Order order);
}