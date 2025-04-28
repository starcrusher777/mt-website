using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<ItemModel, ItemEntity>().ReverseMap();
    }
}