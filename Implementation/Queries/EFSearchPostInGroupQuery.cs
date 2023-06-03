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

        public PageResponse<PostDto> Execute(SearchPostInGroupDto request)
        {
            var posts = _context.Posts.Include(x => x.Likes)
                .Include(x=>x.GroupPosts)
                .Include(x=>x.PhotosVideos)
                .Include(x => x.Comments)
                .ThenInclude(x=>x.Likes)
                .AsQueryable();
            
            PropertyInfo[] properties = typeof(SearchPostInGroupDto).GetProperties();

            posts = posts.StartFiltering<Post>(properties, request);

            return posts.GetResult(request, x => new PostDto
            {
                Id = x.Id,
                PrivacyId = x.PrivacyId,
                Title = x.Title,
                Content = x.Content,
                LikesCounter = x.Likes.Count(),
                CreatedAt = x.CreatedAt,
                Path = x.PhotosVideos.Select(x => x.Path),
                Comments = x.Comments.BuildNestedComments(x.Id)

            });

            //var skipCount = request.PerPage * (request.Page - 1);
            //var response = new PageResponse<PostDto>
            //{
            //    CurrentPage = request.Page,
            //    ItemsPerPage = request.PerPage,
            //    TotalCount = posts.Count(),
            //    Items = posts.Skip(skipCount).Take(request.PerPage).Select(x => new PostDto
            //    {
            //        Id = x.Id,
            //        PrivacyId = x.PrivacyId,
            //        Title = x.Title,
            //        Content = x.Content,
            //        LikesCounter = x.Likes.Count(),
            //        CreatedAt = x.CreatedAt,
            //        Path = x.PhotosVideos.Select(x=>x.Path),
            //        Comments = x.Comments.BuildNestedComments(x.Id)

            //    }).ToList()

            //};
            //return response;

        }
    }
}
