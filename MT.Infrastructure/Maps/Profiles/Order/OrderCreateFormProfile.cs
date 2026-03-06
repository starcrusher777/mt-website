using AutoMapper;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class OrderCreateFormProfile : Profile
{
    public OrderCreateFormProfile()
    {
        CreateMap<OrderCreateFormModel, OrderModel>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item)).ReverseMap();
    }
}