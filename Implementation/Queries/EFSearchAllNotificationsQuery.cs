using Application;
using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFSearchAllNotificationsQuery : ISearchAllNotificationsQuery

    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFSearchAllNotificationsQuery(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 28;

        public string Name => "Searching notifications using EF";

        public PageResponse<NotificationDto> Execute(SearchDto request)
        {
            var notifications = _context.Notifications
                                        .Include(x=>x.User)
                                            .ThenInclude(x=>x.UserProfilePhotos)
                                            .ThenInclude(x=>x.Photo)
                                        .Where(x => x.ReciverId == _actor.Id).AsQueryable();


            notifications = notifications.Where(x => x.isActive).OrderByDescending(x => x.CreatedAt);
            return notifications.GetResult(request, x => new NotificationDto
            {
                Id = x.Id,
                Created = x.CreatedAt,
                Description = x.Description,
                WhoMadeNotification = new NotificationUserDto
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImagesPath = x.User.UserProfilePhotos.Select(x => x.Photo.Path).ToList()
                }
            });

            return new PageResponse<NotificationDto>();
        }
    }
}
