using AutoMapper;
using MT.Domain.Entities;
using MT.Infrastructure.Models;

namespace MT.Infrastructure.Maps.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderModel, OrderEntity>().ReverseMap();
    }
}