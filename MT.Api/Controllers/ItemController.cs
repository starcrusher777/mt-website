using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Contracts.Common;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class ItemController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ItemService _service;

    public ItemController(IMapper mapper, ItemService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (pageSize <= 0) pageSize = 20;
        if (page <= 0) page = 1;
        var (items, totalCount) = await _service.GetItemsPagedAsync(page, pageSize);
        var data = _mapper.Map<List<ItemModel>>(items);
        var paged = new PagedResult<ItemModel>
        {
            Items = data,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
        return Ok(paged);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(long id)
    {
        var item = await _service.GetItemAsync(id);
        return Ok(_mapper.Map<ItemModel>(item));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateItem(ItemModel item)
    {
        item.CreatedAt = DateTime.UtcNow;
        await _service.CreateItemAsync(item);
        return Ok(_mapper.Map<ItemModel>(item));
    }
}
