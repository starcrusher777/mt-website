using AutoMapper;
using MT.Domain.Entities;
using MT.Domain.Exceptions;
using MT.Domain.Interfaces;
using MT.Infrastructure.Models;

namespace MT.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task CreateUserAsync(UserModel userModel)
    {
        var userEntity = _mapper.Map<UserEntity>(userModel);
        await _userRepository.CreateUserAsync(userEntity);
        await _unitOfWork.SaveChangesAsync();
    }
    

    public async Task<List<UserEntity>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        return _mapper.Map<List<UserEntity>>(users);
    }

    public async Task<(List<UserEntity> Users, int TotalCount)> GetUsersPagedAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var take = Math.Max(1, pageSize);
        var users = await _userRepository.GetUsersAsync(skip, take);
        var total = await _userRepository.GetUsersCountAsync();
        return (users, total);
    }

    public async Task<UserEntity> GetUserAsync(long userId)
    {
        var user = await _userRepository.GetUserAsync(userId);
        if (user == null)
            throw new NotFoundException("User", userId);
        return _mapper.Map<UserEntity>(user);
    }

    public async Task<UserEntity> UpdateUserAsync(long userId, UserUpdateModel updatedUser)
    {
        var user = await _userRepository.GetUserAsync(userId);
        if (user == null)
            throw new NotFoundException("User", userId);
        
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
        await _unitOfWork.SaveChangesAsync();
        
        return user;
    }
}