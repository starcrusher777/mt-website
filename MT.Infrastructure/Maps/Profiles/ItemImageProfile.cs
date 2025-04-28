using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class ItemImageProfile : Profile
{
    public ItemImageProfile()
    {
        CreateMap<ItemImageModel, ItemImageEntity>().ReverseMap();
    }
}