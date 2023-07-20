using Application;
using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Core.Extensions;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetAllPostsOfUserFriendsAndGroupsQuery : IGetAllPostsOfUserFriendsAndGroupsQuery
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFGetAllPostsOfUserFriendsAndGroupsQuery(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 29;

        public string Name => "Get Posts For User Main Page using EF";

        public PageResponse<AllPostsDto> Execute(SearchDto request)
        {
            var posts = _context.Posts
                                 .Include(x => x.UserWalls)
                                     .ThenInclude(x => x.User)
                                     .ThenInclude(x => x.UserProfilePhotos)
                                     .ThenInclude(x => x.Photo)
                                 .Include(x => x.Likes)
                                 .Include(x => x.Comments)
                                    .ThenInclude(x => x.Likes)
                                     .Include(x => x.Comments)
                                     .ThenInclude(x => x.User)
                                     .ThenInclude(x => x.UserProfilePhotos)
                                     .ThenInclude(x => x.Photo)
                                 .AsQueryable();



            var friends = _context.Friends
                .Where(x => x.UserId == _actor.Id)
                .Select(x => x.OneFriend).Where(x=>x.isActive).ToList();

            var recFriends = _context.Friends
                .Where(x => x.FriendId == _actor.Id)
                .Select(x => x.User).Where(x => x.isActive).ToList();
            friends.AddRange(recFriends);
            var ids = friends.Select(x => x.Id);

            posts = posts
                .Where(x => x.UserWalls.Any(y => ids.Contains(y.UserId)))
                .OrderBy(x => x.CreatedAt);



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
                Name = x.UserWalls.Select(x => new UserNameDto
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImagesPath = x.User.UserProfilePhotos.Select(x => x.Photo.Path)
                }).FirstOrDefault()
            });

           
        }
    }
}
