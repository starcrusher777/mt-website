using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class PersonalsProfile : Profile
{
    public PersonalsProfile()
    {
        CreateMap<PersonalsModel, PersonalsEntity>().ReverseMap();
    }
}