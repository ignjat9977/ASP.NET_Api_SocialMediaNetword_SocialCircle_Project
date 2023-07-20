using Application;
using Application.Dto;
using Application.Exceptions;
using Application.Queries;
using DataAcess;
using Domain;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<SpecificMessagesDto> Execute(int request)
        {
            var inbox = _context.Messages
                                    .Include(x=>x.Sender)
                                    .Include(x=>x.Reciver)
                                    .Where(x => (x.SenderId == _actor.Id && x.ReciverId == request) ||
                                                (x.SenderId == request && x.ReciverId == _actor.Id))
                                    .OrderByDescending(x=>x.CreatedAt)
                                    .Distinct()
                                    .ToList();

            if(inbox.Count == 0)
            {
                throw new EntityNotFoundException(request, typeof(Message));
            }

            return inbox.Select(x => new SpecificMessagesDto
            {
                SenderId = x.SenderId,
                ReciverId = x.ReciverId,
                ReciverName = x.Reciver.FirstName + " " + x.Reciver.LastName,
                SenderName =  x.Sender.FirstName + " " + x.Sender.LastName,
                CreatedAt = x.CreatedAt,
                Message =x.Content
            }).ToList();


        }
    }
}
