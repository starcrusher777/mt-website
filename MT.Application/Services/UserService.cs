using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task CreateUserAsync(UserModel userModel)
    {
        var userEntity = _mapper.Map<UserEntity>(userModel);
        await _userRepository.CreateUserAsync(userEntity);
    }
    

    public async Task<List<UserEntity>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        return _mapper.Map<List<UserEntity>>(users);
    }

    public async Task<UserEntity?> GetUserAsync(long userId)
    {
        var user = await _userRepository.GetUserAsync(userId);
        return _mapper.Map<UserEntity>(user);
    }

    public async Task<UserEntity?> UpdateUserAsync(long userId, UserUpdateModel updatedUser)
    {
        var user = await _userRepository.GetUserAsync(userId);
        if (user == null) return null;
        
        user.Personals.FirstName = updatedUser.Personals.FirstName;
        user.Personals.LastName = updatedUser.Personals.LastName;
        
        user.Contacts.Email = updatedUser.Contacts.Email;
        user.Contacts.Telephone = updatedUser.Contacts.Telephone;
        user.Contacts.Address = updatedUser.Contacts.Address;
        
        user.Socials.Telegram = updatedUser.Socials.Telegram;
        user.Socials.Twitter = updatedUser.Socials.Twitter;
        user.Socials.Vkontakte = updatedUser.Socials.Vkontakte;
        user.Socials.Instagram = updatedUser.Socials.Instagram;
        
        user.ModifiedAt = DateTime.UtcNow;
        
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        
        return user;
    }
}