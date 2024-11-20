using ProducerService.Service.Abstract;
using ProducerService.Service.Concrete;

namespace ProducerService.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProducerServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        
        return services;
    } 
}