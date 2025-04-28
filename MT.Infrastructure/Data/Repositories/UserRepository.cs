using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;
using MT.Domain.Interfaces;

namespace MT.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationContext context) : IUserRepository
{
    public async Task<List<UserEntity>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }
    
    public async Task<UserEntity?> GetUserAsync(long userId)
    {
        return await context.Users.FindAsync(userId);
    }
    
    public async Task<UserEntity> CreateUserAsync (UserEntity userEntity)
    {
        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();
        return userEntity;
    }
}