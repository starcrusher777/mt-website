using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class UserUpdateProfile : Profile
{
    public UserUpdateProfile()
    {
        CreateMap<UserUpdateModel, UserEntity>().ReverseMap();
        CreateMap<ContactsModel, ContactsEntity>().ReverseMap();
        CreateMap<SocialsModel, SocialsEntity>().ReverseMap();
        CreateMap<PersonalsModel, PersonalsEntity>().ReverseMap();
    }
}