using MT.Domain.Entities;

namespace MT.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderEntity>> GetOrdersAsync();
    Task<OrderEntity> GetOrderAsync(long orderId);
    Task<OrderEntity> CreateOrderAsync(OrderEntity order);
}