using MT.Domain.Entities;

namespace MT.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<UserEntity>> GetUsersAsync();
    Task<UserEntity> GetUserAsync(long userId);
    Task<UserEntity> CreateUserAsync(UserEntity user);
    void Update(UserEntity user);
    Task SaveChangesAsync();
}