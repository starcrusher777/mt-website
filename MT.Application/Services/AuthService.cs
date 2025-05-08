using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository authRepository, IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
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

    public async Task<UserEntity> LoginAsync(LoginModel loginModel)
    {
        return await _authRepository.LoginAsync(loginModel.Email, loginModel.Password);
    }
}