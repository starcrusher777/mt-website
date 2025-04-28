using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class ContactsProfile : Profile
{
    public ContactsProfile()
    {
        CreateMap<ContactsModel, ContactsEntity>().ReverseMap();
    }
}