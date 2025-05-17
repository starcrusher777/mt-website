using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _service;
    private readonly IMapper _mapper;

    public OrderController(OrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _service.GetOrdersAsync();

        return orders.Count == 0 ? Ok("No orders found") : Ok(_mapper.Map<List<OrderModel>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(long orderId)
    {
        var order = await _service.GetOrderAsync(orderId);
        
        return order == null ? Ok($"Order with id '{orderId}' not found!") : Ok(_mapper.Map<OrderModel>(order));
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateOrder(OrderCreateFormModel order)
    {
        order.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        
        await _service.CreateOrderAsync(order);
        
        return Ok(_mapper.Map<OrderCreateFormModel>(order));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrdersByUserIdAsync(long userId)
    {
        var orders = await _service.GetOrdersByUserIdAsync(userId);
        return orders.Count == 0 ? Ok($"User with user id '{userId}' has no orders") : Ok(_mapper.Map<List<OrderModel>>(orders));
    }
    
}