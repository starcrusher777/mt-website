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
    
    public async Task<OrderEntity?> GetOrderAsync(long orderId)
    {
        return await context.Orders.Include(o => o.Item)
            .ThenInclude(i => i.Images)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
    
    public async Task<OrderEntity> CreateOrderAsync (OrderEntity orderEntity)
    {
        await context.Orders.AddAsync(orderEntity);
        await context.SaveChangesAsync();
        return orderEntity;
    }
}