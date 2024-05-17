using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<User, LoginRequestModel>().ReverseMap();
        CreateMap<RegisterRequestModel, User>().ReverseMap();

        CreateMap<CreatePlaceModel, Place>().ReverseMap();
    }
}
