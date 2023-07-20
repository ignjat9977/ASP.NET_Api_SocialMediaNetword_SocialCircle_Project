using Application;
using Application.Dto;
using Application.Exceptions;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFSearchInboxPartsQuery : ISearchInboxPartsQuery
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;
        public EFSearchInboxPartsQuery(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }
        public int Id => 25;

        public string Name => "Searching Inbox parts using EF";

        public PageResponse<UserDto> Execute(SearchDto request)
        {

            var inbox = _context.Messages
                                .Include(x => x.Reciver)
                                .Where(x => (x.SenderId == _actor.Id) || (x.ReciverId == _actor.Id))
                                .Distinct()
                                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                inbox = inbox
                    .Where(x => x.Reciver.FirstName.ToLower().Contains(request.Keyword.ToLower())
                             || x.Reciver.LastName.ToLower().Contains(request.Keyword.ToLower()));
            }

            var correspondents = inbox
                .OrderByDescending(x=>x.CreatedAt)
                .Select(x => x.SenderId)
                .Union(inbox.Select(x => x.ReciverId))
                .Where(x => x != _actor.Id)
                .Distinct()
                .ToList();

            if(request.PerPage <= 0)
            {
                request.PerPage = 10;
            }

            if (request.Page <= 0)
            {
                request.Page = 1;
            }
            var skipCount = request.PerPage * (request.Page - 1);

            var users = _context.Users
                                .Include(x=>x.UserProfilePhotos)
                                .ThenInclude(x=>x.Photo)
                                .Where(x => correspondents.Contains(x.Id));

            var response = new PageResponse<UserDto>
            {
                CurrentPage = request.Page,
                ItemsPerPage = request.PerPage,
                TotalCount = users.Count(),
                Items = users.Skip(skipCount).Take(request.PerPage).Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ImagesPath = x.UserProfilePhotos.Select(x=>x.Photo.Path),

                }).ToList()

            };


            return response;
        }
    }
}
