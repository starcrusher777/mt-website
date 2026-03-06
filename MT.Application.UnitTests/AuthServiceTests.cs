using Microsoft.Extensions.Configuration;
using MT.Application.Services;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;
using NSubstitute;
using Xunit;

namespace MT.Application.UnitTests;

public class AuthServiceTests
{
    private readonly IAuthRepository _authRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    public AuthServiceTests()
    {
        _authRepository = Substitute.For<IAuthRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _config = Substitute.For<IConfiguration>();
        _config["Jwt:Key"].Returns(new string('x', 32));
        _config["Jwt:Issuer"].Returns("Test");
        _config["Jwt:Audience"].Returns("Test");
    }

    [Fact]
    public async Task LoginAsync_WhenInvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        _authRepository.LoginAsync(Arg.Any<string>(), Arg.Any<string>()).Returns((UserEntity?)null);
        var sut = new AuthService(_authRepository, _unitOfWork, Substitute.For<AutoMapper.IMapper>(), _config);
        var loginModel = new LoginModel { Email = "a@b.com", Password = "wrong" };

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => sut.LoginAsync(loginModel));
    }

    [Fact]
    public async Task LoginAsync_WhenValidCredentials_ReturnsTokenAndUser()
    {
        var user = new UserEntity { Id = 1, Email = "a@b.com", Username = "user" };
        _authRepository.LoginAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(user);
        var sut = new AuthService(_authRepository, _unitOfWork, Substitute.For<AutoMapper.IMapper>(), _config);
        var loginModel = new LoginModel { Email = "a@b.com", Password = "pass" };

        var (token, returnedUser) = await sut.LoginAsync(loginModel);

        Assert.NotNull(token);
        Assert.NotEmpty(token);
        Assert.Equal(user.Id, returnedUser.Id);
        Assert.Equal(user.Email, returnedUser.Email);
    }
}
