using Application.Dto;
using Application.Queries;
using Application.Reflection;
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
    public class EFSearchPostUserWallQuery : ISearchPostUserWallQuery
    {
        private MyDbContext _context;

        public EFSearchPostUserWallQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 19;

        public string Name => "EF Query search posts";

        public PageResponse<PostDto> Execute(SearchPostDto request)
        {

            var posts = _context.Posts
                                 .Include(x => x.Likes)
                                 .Include(x => x.Comments)
                                 .ThenInclude(x=>x.Likes)
                                 .Include(x=>x.Comments)
                                 .ThenInclude(x=>x.User)
                                 .ThenInclude(x=>x.UserProfilePhotos)
                                 .ThenInclude(x=>x.Photo)
                                 .AsQueryable();



            var properties = typeof(SearchPostDto).GetProperties();

            posts = posts.StartFiltering<Post>(properties, request);


            return posts.GetResult(request, x => new PostDto
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
                Comments = x.Comments.BuildNestedComments(null)

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
            //        Path = x.PhotosVideos.Select(x=>x.Path),
            //        PrivacyId = x.PrivacyId,
            //        Title = x.Title,
            //        Content = x.Content,
            //        LikesCounter = x.Likes.Count(),
            //        CreatedAt = x.CreatedAt,
            //        CommentsCounter = x.Comments.Count(),
            //        WhichFile = x.PhotosVideos.Select(x=>x.WhichFile),
            //        Comments = x.Comments.BuildNestedComments(null)

            //    }).ToList()

            //};
            //return response;
        }

    }
}
