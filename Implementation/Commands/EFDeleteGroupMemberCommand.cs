using Application;
using Application.Commands;
using Application.Exceptions;
using DataAcess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFDeleteGroupMemberCommand : IDeleteGroupMemberCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFDeleteGroupMemberCommand(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id =>21;

        public string Name => "Deleting groupMember using EF";

        public void Execute(int request)
        {
            var groupMember = _context.GroupMembers.Where(x => x.GroupId == request && x.UserId == _actor.Id).FirstOrDefault();

            if (groupMember == null)
                throw new EntityNotFoundException(request, typeof(GroupMember));

            _context.GroupMembers.Remove(groupMember);
            _context.SaveChanges();
        }
    }
}
