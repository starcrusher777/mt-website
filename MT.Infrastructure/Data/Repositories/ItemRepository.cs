using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;
using MT.Domain.Interfaces;

namespace MT.Infrastructure.Data.Repositories;

public class ItemRepository(ApplicationContext context) : IItemRepository
{
    public async Task<List<ItemEntity>> GetItemsAsync()
    {
        return await context.Items.ToListAsync();
    }

    public async Task<List<ItemEntity>> GetItemsAsync(int skip, int take)
    {
        return await context.Items
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<int> GetItemsCountAsync()
    {
        return await context.Items.CountAsync();
    }
    
    public async Task<ItemEntity?> GetItemAsync(long itemId)
    {
        return await context.Items.FindAsync(itemId);
    }
    
    public async Task<ItemEntity> CreateItemAsync(ItemEntity itemEntity)
    {
        await context.Items.AddAsync(itemEntity);
        return itemEntity;
    }
}