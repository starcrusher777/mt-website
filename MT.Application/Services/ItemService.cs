using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Exceptions;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class ItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task CreateItemAsync(ItemModel itemModel)
    {
        var itemEntity = _mapper.Map<ItemEntity>(itemModel);
        await _itemRepository.CreateItemAsync(itemEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ItemEntity>> GetItemsAsync()
    {
        var items = await _itemRepository.GetItemsAsync();
        return _mapper.Map<List<ItemEntity>>(items);
    }

    public async Task<(List<ItemEntity> Items, int TotalCount)> GetItemsPagedAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var take = Math.Max(1, pageSize);
        var items = await _itemRepository.GetItemsAsync(skip, take);
        var total = await _itemRepository.GetItemsCountAsync();
        return (items, total);
    }

    public async Task<ItemEntity> GetItemAsync(long itemId)
    {
        var item = await _itemRepository.GetItemAsync(itemId);
        if (item == null)
            throw new NotFoundException("Item", itemId);
        return _mapper.Map<ItemEntity>(item);
    }
}