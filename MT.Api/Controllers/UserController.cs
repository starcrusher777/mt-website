using System.Security.Claims;
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
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserService _service;

    public UserController(UserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (pageSize <= 0) pageSize = 20;
        if (page <= 0) page = 1;
        var (users, totalCount) = await _service.GetUsersPagedAsync(page, pageSize);
        var data = _mapper.Map<List<UserModel>>(users);
        var paged = new PagedResult<UserModel>
        {
            Items = data,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
        return Ok(paged);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(long id)
    {
        var user = await _service.GetUserAsync(id);
        return Ok(_mapper.Map<UserModel>(user));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel user)
    {
        user.CreatedAt = DateTime.UtcNow;
        await _service.CreateUserAsync(user);
        user.Password = string.Empty;
        return Ok(_mapper.Map<UserModel>(user));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromRoute(Name = "id")] long userId, [FromBody] UserUpdateModel updatedUser)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdFromToken == null || userIdFromToken != userId.ToString())
            return Unauthorized();
        var user = await _service.UpdateUserAsync(userId, updatedUser);
        return Ok(_mapper.Map<UserModel>(user));
    }
}
