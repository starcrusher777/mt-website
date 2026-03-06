using Microsoft.Extensions.Caching.Memory;
using MT.Application.Services;
using MT.Domain.Entities;
using MT.Domain.Exceptions;
using MT.Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace MT.Application.UnitTests;

public class ItemServiceTests
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AutoMapper.IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ItemServiceTests()
    {
        _itemRepository = Substitute.For<IItemRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<AutoMapper.IMapper>();
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    [Fact]
    public async Task GetItemAsync_WhenItemNotFound_ThrowsNotFoundException()
    {
        _itemRepository.GetItemAsync(Arg.Any<long>()).Returns((ItemEntity?)null);
        var sut = new ItemService(_itemRepository, _unitOfWork, _mapper, _cache);

        var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.GetItemAsync(999));

        Assert.Contains("Item", ex.Message);
        Assert.Contains("999", ex.Message);
    }

    [Fact]
    public async Task GetItemAsync_WhenItemExists_ReturnsMappedEntity()
    {
        var entity = new ItemEntity { Id = 1, ItemId = 1, Name = "Test", Description = "D" };
        _itemRepository.GetItemAsync(1).Returns(entity);
        _mapper.Map<ItemEntity>(Arg.Any<object>()).Returns(entity);
        var sut = new ItemService(_itemRepository, _unitOfWork, _mapper, _cache);

        var result = await sut.GetItemAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test", result.Name);
    }
}
