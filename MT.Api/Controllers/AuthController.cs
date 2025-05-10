using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        try
        {
            var (token, user) = await _service.LoginAsync(model);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            return Ok(new { message = "Вход успешен", username = user.Username, userid = user.Id, token });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult GetUser()
    {
        var username = User.Identity?.Name;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        return Ok(new
        {
            Username = username, 
            Email = email, 
            Id = id
        });
    }
}