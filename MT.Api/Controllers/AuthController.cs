using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MT.Application.Services;
using MT.Infrastructure.Models;

namespace MerchTrade.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
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
        return Ok("Registration completed successfully");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var (token, user) = await _service.LoginAsync(model);
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(30)
        });
        return Ok(new { message = "Login successful", username = user.Username, userid = user.Id, role = user.Role.ToString(), token });
    }

    [HttpPost]
    [Authorize]
    public IActionResult GetUser()
    {
        var username = User.Identity?.Name;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(new { Username = username, Email = email, Id = id });
    }
}
