using Application.Dto;
using Application.Queries;
using DataAcess;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetFriendsAndFriendsOfFriendsQuery : IGetFriendsAndFriendsOfFriendsQuery
    {
        private readonly MyDbContext _context;

        public EFGetFriendsAndFriendsOfFriendsQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 15;

        public string Name => "Get a friends of user and their friends using EF";

        public FriendAndFriends Execute(UserIdDto request)
        {
            var friends = _context.Friends.Include(x=>x.User)
                .ThenInclude(x=>x.UserProfilePhotos).ThenInclude(x=>x.Photo)
                .Where(x => x.UserId == request.Id)
                .Select(x => x.OneFriend).ToList();

            var recFriends = _context.Friends.Include(x => x.User)
                .ThenInclude(x => x.UserProfilePhotos).ThenInclude(x => x.Photo)
                .Where(x => x.FriendId == request.Id)
                .Select(x => x.User).ToList();
            friends.AddRange(recFriends);
            var ids = friends.Select(x => x.Id);

            var friendOfFriends = _context.Friends.Include(x => x.User)
                .ThenInclude(x => x.UserProfilePhotos).ThenInclude(x => x.Photo)
                .Where(x => ids.Contains(x.UserId))
                .Select(x => x.OneFriend).ToList();

            var recFriendOfFriends = _context.Friends.Include(x => x.User)
                .ThenInclude(x => x.UserProfilePhotos).ThenInclude(x => x.Photo)
                .Where(x => ids.Contains(x.FriendId))
                .Select(x => x.User).ToList();

            friendOfFriends.AddRange(recFriendOfFriends);
            var fof = friendOfFriends.Except(friends).Distinct().AsQueryable();
            fof = fof.Include(x => x.UserProfilePhotos).ThenInclude(x => x.Photo);




            return new FriendAndFriends
            {
                Friends = friends.Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    RoleId = x.RoleId,
                    ImagesPath = x.UserProfilePhotos
                                    .Select(x => x.Photo.Path)
                }),
                FriendsOf = fof.Select(x=>new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    RoleId = x.RoleId,
                    ImagesPath = x.UserProfilePhotos
                                    .Select(x => x.Photo.Path)
                })
            };
        }
    }
}
