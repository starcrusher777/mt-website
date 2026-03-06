using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;
using MT.Domain.Interfaces;

namespace MT.Infrastructure.Data.Repositories;

public class OrderRepository(ApplicationContext context) : IOrderRepository
{
    public async Task<List<OrderEntity>> GetOrdersAsync()
    {
        return await context.Orders
            .Include(o => o.Item)
            .ThenInclude(i => i.Images)
            .ToListAsync();
    }

    public async Task<List<OrderEntity>> GetOrdersAsync(int skip, int take)
    {
        return await context.Orders
            .Include(o => o.Item)
            .ThenInclude(i => i.Images)
            .OrderBy(o => o.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<int> GetOrdersCountAsync()
    {
        return await context.Orders.CountAsync();
    }
    
    public async Task<OrderEntity?> GetOrderAsync(long orderId)
    {
        return await context.Orders.Include(o => o.Item)
            .ThenInclude(i => i.Images)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
    
    public async Task<OrderEntity> CreateOrderAsync(OrderEntity orderEntity)
    {
        await context.Orders.AddAsync(orderEntity);
        return orderEntity;
    }

    public async Task<List<OrderEntity>> GetOrdersByUserIdAsync(long userId)
    {
        return await context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Item)
            .ThenInclude(i => i.Images)
            .Include(o => o.User)
            .ToListAsync();
    }

    public void UpdateOrder(OrderEntity orderEntity)
    {
        context.Orders.Update(orderEntity);
    }
}