using AutoMapper;
using Domain;
using ProjectNetworkMediaApi.Dto;

namespace ProjectNetworkMediaApi.Core
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();
        }
    }
}
