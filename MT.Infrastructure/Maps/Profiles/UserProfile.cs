using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserModel, UserEntity>().ReverseMap();
    }
}