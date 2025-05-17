using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    

    public async Task CreateOrderAsync(OrderCreateFormModel orderModel)
    {
        var order = new OrderEntity
        {
            OrderName = orderModel.OrderName,
            Status = orderModel.Status,
            Type = orderModel.Type,
            UserId = orderModel.UserId,
            Item = new ItemEntity
            {
                Name = orderModel.Item.Name,
                Description = orderModel.Item.Description,
                Price = orderModel.Item.Price,
                Quantity = orderModel.Item.Quantity,
                Images = new List<ItemImageEntity>()
            }
        };

        if (orderModel.Images != null)
        {
            Console.WriteLine($"Получено файлов: {orderModel.Images.Length}");
            
            foreach (var file in orderModel.Images)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine("wwwroot/images", fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                order.Item.Images.Add(new ItemImageEntity
                {
                    ImageUrl = $"/images/{fileName}"
                });
            }
        }
        else
        {
            Console.WriteLine("Файлы не получены");
        }

        await _orderRepository.CreateOrderAsync(order);
    }

    public async Task<List<OrderEntity>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetOrdersAsync();
        return _mapper.Map<List<OrderEntity>>(orders);
    }

    public async Task<OrderEntity?> GetOrderAsync(long orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        return _mapper.Map<OrderEntity>(order);
    }

    public async Task<List<OrderEntity>> GetOrdersByUserIdAsync(long userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        return orders;
    }
}