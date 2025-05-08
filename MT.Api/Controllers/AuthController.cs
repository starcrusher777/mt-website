using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[Route("api/[controller]/[action]")]
[ApiController] 
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AuthService _service;

    public AuthController(IMapper mapper, AuthService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        await _service.RegisterAsync(model);
        return Ok("Регистрация прошла успешно");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _service.LoginAsync(model);
        return Ok(new { userId = user.Id });
    }
}