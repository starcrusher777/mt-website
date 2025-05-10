using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AuthService(IAuthRepository authRepository, IMapper mapper, IConfiguration config)
    {
        _authRepository = authRepository;
        _mapper = mapper;
        _config = config;
    }
    
    public async Task RegisterAsync(RegisterModel registerModel)
    {
        var user = new UserEntity
        {
            Username = registerModel.Username,
            Email = registerModel.Email
        };
        
        await _authRepository.RegisterAsync(user, registerModel.Password);
    }

    public async Task<(string token, UserEntity user)> LoginAsync(LoginModel loginModel)
    {
        var user = await _authRepository.LoginAsync(loginModel.Email, loginModel.Password);
        if (user == null)
            throw new UnauthorizedAccessException("Неверный email или пароль");
        var token = GenerateJwtToken(user);
        return (token, user);
    }
    
    private string GenerateJwtToken(UserEntity user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}