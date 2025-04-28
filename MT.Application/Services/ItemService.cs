using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class ItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    
    public ItemService(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }
    
    public async Task CreateItemAsync(ItemModel itemModel)
    {
        var itemEntity = _mapper.Map<ItemEntity>(itemModel);
        await _itemRepository.CreateItemAsync(itemEntity);
    }

    public async Task<List<ItemEntity>> GetItemsAsync()
    {
        var items = await _itemRepository.GetItemsAsync();
        return _mapper.Map<List<ItemEntity>>(items);
    }

    public async Task<ItemEntity?> GetItemAsync(long itemId)
    {
        var item = await _itemRepository.GetItemAsync(itemId);
        return _mapper.Map<ItemEntity>(item);
    }
}