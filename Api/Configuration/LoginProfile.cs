using AutoMapper;
using BETSoftware.Domain.Models.Dtos;
using BETSoftware.Domain.Models;

namespace Api.Configuration
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<Login, LoginInDto>().ReverseMap();
        }
    }
}
