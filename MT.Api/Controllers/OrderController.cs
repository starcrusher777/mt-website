using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Contracts.Common;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
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
    public async Task<IActionResult> GetOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (pageSize <= 0) pageSize = 20;
        if (page <= 0) page = 1;
        var (orders, totalCount) = await _service.GetOrdersPagedAsync(page, pageSize);
        var data = _mapper.Map<List<OrderModel>>(orders);
        var paged = new PagedResult<OrderModel>
        {
            Items = data,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
        return Ok(paged);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(long orderId)
    {
        var order = await _service.GetOrderAsync(orderId);
        return Ok(_mapper.Map<OrderModel>(order));
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateOrder(OrderCreateFormModel order)
    {
        order.CreatedAt = DateTime.UtcNow;
        await _service.CreateOrderAsync(order);
        return Ok(_mapper.Map<OrderCreateFormModel>(order));
    }

    [HttpGet("by-user/{id}")]
    public async Task<IActionResult> GetOrdersByUserId(long id)
    {
        var orders = await _service.GetOrdersByUserIdAsync(id);
        return Ok(_mapper.Map<List<OrderModel>>(orders));
    }

    [HttpPut("{orderId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateOrder(long orderId, [FromForm] OrderUpdateModel updatedOrder)
    {
        var order = await _service.UpdateOrderAsync(orderId, updatedOrder);
        return Ok(_mapper.Map<OrderModel>(order));
    }
}
