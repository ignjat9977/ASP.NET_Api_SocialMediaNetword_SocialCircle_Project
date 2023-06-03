using AutoMapper;
using Domain;
using ProjectNetworkMediaApi.Dto;

namespace ProjectNetworkMediaApi.Core
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostDto>();

            CreateMap<PostDto, Post>();
        }
    }
}
