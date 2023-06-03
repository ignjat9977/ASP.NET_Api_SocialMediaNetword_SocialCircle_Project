using Application;
using Application.Dto;
using Application.Exceptions;
using Application.Queries;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetSpecificInboxPartQuery : IGetSpecificInboxPartQuery
    {
        private readonly IApplicationActor _actor;
        private readonly MyDbContext _context;

        public EFGetSpecificInboxPartQuery(IApplicationActor actor, MyDbContext context)
        {
            _actor = actor;
            _context = context;
        }

        public int Id => 26;

        public string Name => "Get specific inbox using EF";

        public SpecificMessagesDto Execute(int request)
        {
            var inbox = _context.Messages
                .Where(x => x.SenderId == _actor.Id && x.ReciverId == request);

            if (inbox == null)
                throw new EntityNotFoundException(request, typeof(Message));

            return new SpecificMessagesDto
            {
                Messages = inbox.Select(x => new MessDto
                {
                    Content = x.Content,
                    CreatedAt = x.CreatedAt
                }).ToList()
            };
        }
    }
}
