using AutoMapper;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class ItemViewProfile : Profile
{
    public ItemViewProfile()
    {
        CreateMap<ItemViewModel, ItemModel>().ReverseMap();
    }
}