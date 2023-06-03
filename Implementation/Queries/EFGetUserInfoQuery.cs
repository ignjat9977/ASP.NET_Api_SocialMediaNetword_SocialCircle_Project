using Application.Exceptions;
using Application.Queries;
using DataAcess;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetUserInfoQuery : IGetUserInfoQuery
    {
        private readonly MyDbContext _context;

        public EFGetUserInfoQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 17;

        public string Name => "Get User Info using EF";

        public UserDto Execute(int request)
        {
            var user = _context.Users
                .Include(x=>x.UserProfilePhotos)
                .ThenInclude(x=>x.Photo)
                .Include(x=>x.Senters)
                .Include(x=>x.Recivers)
                .FirstOrDefault(x => x.Id == request);

            if (user == null)
                throw new EntityNotFoundException(request, typeof(User));

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                ImagesPath = user.UserProfilePhotos.Select(x=>x.Photo.Path),
                NumberOfFriends = user.Senters.Count() + user.Recivers.Count()
            };
        }
    }
}
