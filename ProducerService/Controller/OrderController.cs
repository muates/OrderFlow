using Microsoft.AspNetCore.Mvc;
using ProducerService.Service.Abstract;
using SharedLibrary.Model;

namespace ProducerService.Controller;

[Route("api/v1/order")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult> CreateOrder(Order order)
    {
        await _orderService.CreateOrderAsync(order);
        
        return Ok("Success");
    }
}