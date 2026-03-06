using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using MT.Domain.Entities;
using MT.Domain.Exceptions;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class ItemService
{
    private const string CacheKeyPrefix = "items";
    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(2);

    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
    {
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task CreateItemAsync(ItemModel itemModel)
    {
        var itemEntity = _mapper.Map<ItemEntity>(itemModel);
        await _itemRepository.CreateItemAsync(itemEntity);
        await _unitOfWork.SaveChangesAsync();
        _cache.Remove($"{CacheKeyPrefix}:all");
    }

    public async Task<List<ItemEntity>> GetItemsAsync()
    {
        var key = $"{CacheKeyPrefix}:all";
        if (_cache.TryGetValue(key, out (List<ItemEntity> Items, int _) cached))
            return cached.Items;
        var items = await _itemRepository.GetItemsAsync();
        var result = _mapper.Map<List<ItemEntity>>(items);
        _cache.Set(key, (result, 0), CacheTtl);
        return result;
    }

    public async Task<(List<ItemEntity> Items, int TotalCount)> GetItemsPagedAsync(int page, int pageSize)
    {
        var key = $"{CacheKeyPrefix}:paged:{page}:{pageSize}";
        if (_cache.TryGetValue(key, out (List<ItemEntity> Items, int TotalCount) cached))
            return cached;
        var skip = (page - 1) * pageSize;
        var take = Math.Max(1, pageSize);
        var items = await _itemRepository.GetItemsAsync(skip, take);
        var total = await _itemRepository.GetItemsCountAsync();
        var result = (items, total);
        _cache.Set(key, result, CacheTtl);
        return result;
    }

    public async Task<ItemEntity> GetItemAsync(long itemId)
    {
        var item = await _itemRepository.GetItemAsync(itemId);
        if (item == null)
            throw new NotFoundException("Item", itemId);
        return _mapper.Map<ItemEntity>(item);
    }
}