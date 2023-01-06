using AutoMapper;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;

namespace Api.Configuration
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductInDto>().ReverseMap();
            CreateMap<Product, ProductOutDto>().ReverseMap();
        }
    }
}
