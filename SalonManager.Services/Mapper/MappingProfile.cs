using AutoMapper;
using SalonManager.Domain.Entities;
using SalonManager.Services.Services.Users;

namespace SalonManager.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<UserResponse, UserRequest>().ReverseMap();
        }
    }
}
