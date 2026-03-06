using MT.Domain.Entities;

namespace MT.Domain.Interfaces;

public interface IItemRepository
{
    Task<List<ItemEntity>> GetItemsAsync();
    Task<List<ItemEntity>> GetItemsAsync(int skip, int take);
    Task<int> GetItemsCountAsync();
    Task<ItemEntity?> GetItemAsync(long itemId);
    Task<ItemEntity> CreateItemAsync(ItemEntity item);
}