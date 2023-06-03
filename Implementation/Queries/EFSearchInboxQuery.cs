using Application;
using Application.Dto;
using Application.Queries;
using DataAcess;
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
    public class EFSearchInboxQuery : ISearchInboxQuery
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;
        public EFSearchInboxQuery(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }
        public int Id => 24;

        public string Name => "Searching inbox using dto";

        public PageResponseD<MessagesDto> Execute(SearchDto request)
        {
            var messages = _context.Messages.Include(x=>x.Reciver).Where(x => x.SenderId == _actor.Id || x.ReciverId == _actor.Id).AsQueryable().ToList();
            var reciversIds = messages
                .OrderByDescending(x => x.CreatedAt).OrderBy(x => x.ReciverId)
                .Select(x => x.ReciverId);
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                reciversIds = messages
                    .Where(x => x.Reciver.FirstName.ToLower().Contains(request.Keyword.ToLower()))
                    .OrderByDescending(x => x.CreatedAt).OrderBy(x => x.ReciverId)
                    .Select(x => x.ReciverId);
            }
                
            Dictionary<int, List<MessagesDto>> dic = new Dictionary<int, List<MessagesDto>>();

            foreach (var id in reciversIds)
            {
                dic[id] = messages.Where(x => x.ReciverId == id).Select(x => new MessagesDto
                {
                    SenderId = x.SenderId,
                    ReciverId = x.ReciverId,
                    Message = x.Content
                }).ToList();
            }

            var skipCount = request.PerPage * (request.Page - 1);
            var response = new PageResponseD<MessagesDto>
            {
                CurrentPage = request.Page,
                ItemsPerPage = request.PerPage,
                TotalCount = messages.Count(),
                Items = dic

            };
            return response;
        }
    }
}
