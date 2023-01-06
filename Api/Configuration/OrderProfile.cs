using AutoMapper;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;

namespace Api.Configuration
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderInDto>().ReverseMap();
            CreateMap<Order, OrderOutDto>().ReverseMap();
        }
    }
}
