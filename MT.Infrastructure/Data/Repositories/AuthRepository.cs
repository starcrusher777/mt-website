using Microsoft.EntityFrameworkCore;
using MT.Domain.Entities;
using MT.Domain.Interfaces;

namespace MT.Infrastructure.Data.Repositories;

public class AuthRepository(ApplicationContext context) : IAuthRepository
{
    public async Task RegisterAsync(UserEntity user, string password)
    {
        if (await context.Users.AnyAsync(u => u.Email == user.Email))
            throw new Exception("Пользователь уже существует");

        CreatePasswordHash(password, out byte[] hash, out byte[] salt);

        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<UserEntity> LoginAsync(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            throw new Exception("Неверный логин");

        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Неверный пароль");

        return user; // или user.Id.ToString(), если нужен ID
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }
}
