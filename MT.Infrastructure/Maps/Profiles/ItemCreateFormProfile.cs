using AutoMapper;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class ItemCreateFormProfile : Profile
{
    public ItemCreateFormProfile()
    {
        CreateMap<ItemCreateFormModel, ItemModel>()
            .ForMember(dest => dest.Images, opt => opt.Ignore()).ReverseMap();
    }
}