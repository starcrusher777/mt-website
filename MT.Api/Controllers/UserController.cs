using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
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
    public async Task<IActionResult> GetUsers()
    {
        var users = await _service.GetUsersAsync();

        return users.Count == 0 ? Ok("No users found") : Ok(_mapper.Map<List<UserModel>>(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(long userId)
    {
        var user = await _service.GetUserAsync(userId);
        
        if (user == null)
        {
            return NotFound($"User with id '{userId}' not found!");
        }

        return Ok(_mapper.Map<UserModel>(user));
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserModel user)
    {
        user.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        
        await _service.CreateUserAsync(user);
        
        user.Password = null;
        
        return Ok(_mapper.Map<UserModel>(user));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute(Name = "id")] long userId, [FromBody] UserUpdateModel updatedUser)
    {
        var user = await _service.UpdateUserAsync(userId, updatedUser);
        if (user == null)
            return Ok($"User with id '{userId}' not found!");
        
        return Ok(_mapper.Map<UserModel>(user));
    }
}