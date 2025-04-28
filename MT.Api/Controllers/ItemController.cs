using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
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
    public async Task<IActionResult> GetItems()
    {
        var items = await _service.GetItemsAsync();

        if (!items.Any())
        {
            return Ok("No items found");
        }
        
        return Ok(_mapper.Map<List<ItemModel>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(long itemId)
    {
        var item = await _service.GetItemAsync(itemId);
        
        if (item == null)
        {
            return NotFound($"Item with id '{itemId}' not found!");
        }

        return Ok(_mapper.Map<ItemModel>(item));
    }

    [HttpPost]
    public async Task<ActionResult> CreateItem(ItemModel item)
    {
        item.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        
        await _service.CreateItemAsync(item);
        
        return Ok(_mapper.Map<ItemModel>(item));
    }
}