using MT.Domain.Entities;

namespace MT.Domain.Interfaces;

public interface IAuthRepository
{
    Task RegisterAsync(UserEntity user, string password);
    Task<UserEntity> LoginAsync(string email, string password);
    
    
}