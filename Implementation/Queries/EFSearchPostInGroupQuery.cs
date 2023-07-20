using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using Implementation.Reflection;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Core.Extensions;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFSearchPostInGroupQuery : ISearchPostInGroupQuery
    {

        private MyDbContext _context;

        public EFSearchPostInGroupQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Search Post in group using EF command!";

        public PageResponse<AllPostsDto> Execute(SearchPostInGroupDto request)
        {
            var posts = _context.Posts.Include(x => x.Likes)
                .Include(x=>x.GroupPosts)
                    .ThenInclude(x=>x.User)
                    .ThenInclude(x=>x.UserProfilePhotos)
                    .ThenInclude(x=>x.Photo)
                .Include(x=>x.PhotosVideos)
                .Include(x => x.Comments)
                    .ThenInclude(x=>x.User)
                    .ThenInclude(x => x.UserProfilePhotos)
                    .ThenInclude(x => x.Photo)
                .Include(x => x.Comments)
                    .ThenInclude(x=>x.Likes)
                .AsQueryable();
            
            PropertyInfo[] properties = typeof(SearchPostInGroupDto).GetProperties();

            posts = posts.StartFiltering<Post>(properties, request);
            posts = posts.OrderByDescending(x => x.CreatedAt);
            return posts.GetResult(request, x => new AllPostsDto
            {
                Id = x.Id,
                Path = x.PhotosVideos.Select(x => x.Path),
                PrivacyId = x.PrivacyId,
                Title = x.Title,
                Content = x.Content,
                LikesCounter = x.Likes.Count(),
                CreatedAt = x.CreatedAt,
                CommentsCounter = x.Comments.Count(),
                WhichFile = x.PhotosVideos.Select(x => x.WhichFile),
                Comments = x.Comments.BuildNestedComments(null),
                Name = x.GroupPosts.Select(x => new UserNameDto
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImagesPath = x.User.UserProfilePhotos.Select(x => x.Photo.Path)
                }).FirstOrDefault()
            });

        }
    }
}
