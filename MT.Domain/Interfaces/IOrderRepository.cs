using MT.Domain.Entities;

namespace MT.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderEntity>> GetOrdersAsync();
    Task<List<OrderEntity>> GetOrdersAsync(int skip, int take);
    Task<int> GetOrdersCountAsync();
    Task<OrderEntity?> GetOrderAsync(long orderId);
    Task<OrderEntity> CreateOrderAsync(OrderEntity order);
    Task<List<OrderEntity>> GetOrdersByUserIdAsync(long userId);
    void UpdateOrder(OrderEntity order);
}