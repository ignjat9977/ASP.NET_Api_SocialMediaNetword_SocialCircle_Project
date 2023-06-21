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

        public PageResponse<InboxPartDto> Execute(SearchDto request)
        {
            var messages = _context.Messages
                .Include(x => x.Reciver)
                .Where(x => x.SenderId == _actor.Id || x.ReciverId == _actor.Id)
                .OrderByDescending(x => x.CreatedAt)
                .OrderBy(x => x.ReciverId)
                .AsQueryable();


            if (!string.IsNullOrEmpty(request.Keyword))
            {
                messages = messages
                    .Where(x => x.Reciver.FirstName.ToLower().Contains(request.Keyword.ToLower())
                             || x.Reciver.LastName.ToLower().Contains(request.Keyword.ToLower()));
            }

            //return messages.GetResult(request, x => new InboxPartDto
            //{
            //    SenderId = x.SenderId,
            //    ReciverId = x.ReciverId,
            //    SenderName = $"{x.Sender.FirstName} {x.Sender.LastName}",
            //    ReciverName = $"{x.Reciver.FirstName} {x.Reciver.LastName}",
            //    FirstThreeMessages = messages
            //            .Take(3)
            //            .Where(y => y.ReciverId == x.ReciverId && y.SenderId == x.SenderId)
            //            .Select(c => new MessDto
            //            {
            //                Content = c.Content,
            //                CreatedAt = c.CreatedAt
            //            })
            //});



            var skipCount = request.PerPage * (request.Page - 1);
            var response = new PageResponse<InboxPartDto>
            {
                CurrentPage = request.Page,
                ItemsPerPage = request.PerPage,
                TotalCount = messages.Count(),
                Items = messages.OrderByDescending(x=>x.CreatedAt).Select(x => new InboxPartDto
                {
                    SenderId = x.SenderId,
                    ReciverId = x.ReciverId,
                    SenderName = $"{x.Sender.FirstName} {x.Sender.LastName}",
                    ReciverName = $"{x.Reciver.FirstName} {x.Reciver.LastName}",
                    FirstThreeMessages = messages
                        .Where(y=> (y.SenderId == x.SenderId && y.ReciverId ==  x.ReciverId) 
                                   || (y.ReciverId == x.SenderId && y.SenderId == x.ReciverId))
                        .OrderBy(x=>x.CreatedAt)
                        .Select(c => new MessDto
                        {
                            SenderId = c.SenderId,
                            Sender = c.Sender.FirstName + " " + c.Sender.LastName,
                            Content = c.Content,
                            CreatedAt = c.CreatedAt
                        }).ToList()
                }).ToList()

            };
            return response;
        }
    }
}
