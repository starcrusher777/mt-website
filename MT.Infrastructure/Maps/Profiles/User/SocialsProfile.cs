using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class SocialsProfile : Profile
{
    public SocialsProfile()
    {
        CreateMap<SocialsModel, SocialsEntity>().ReverseMap();
    }
}