using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class OrderUpdateProfile : Profile
{
    public OrderUpdateProfile()
    {
        CreateMap<OrderUpdateModel, OrderEntity>().ReverseMap();
    }
}