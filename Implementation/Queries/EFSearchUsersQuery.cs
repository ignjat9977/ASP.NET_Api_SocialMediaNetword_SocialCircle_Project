using Application.Dto;
using Application.Queries;
using DataAcess;
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
    public class EFSearchUsersQuery : ISearchUsersQuery
    {
        private readonly MyDbContext _context;

        public EFSearchUsersQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 23;

        public string Name => "Search users using EF";

        public PageResponse<UserDto> Execute(SearchDto request)
        {
            var users = _context.Users.Include(x => x.UserProfilePhotos).ThenInclude(x => x.Photo).AsQueryable();

           

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var keyword = request.Keyword.ToLower();
                users = users.Where(x => x.FirstName.ToLower().Contains(keyword) || x.LastName.ToLower().Contains(keyword));
            }
            users = users.Where(x => x.isActive);

            return users.GetResult(request, x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImagesPath = x.UserProfilePhotos.Select(x => x.Photo.Path),
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                RoleId = x.RoleId

            });
        }
    }
}
