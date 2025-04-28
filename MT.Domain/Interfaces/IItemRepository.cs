using MT.Domain.Entities;

namespace MT.Domain.Interfaces;

public interface IItemRepository
{
    Task<List<ItemEntity>> GetItemsAsync();
    Task<ItemEntity> GetItemAsync(long itemId);
    Task<ItemEntity> CreateItemAsync(ItemEntity item);
}