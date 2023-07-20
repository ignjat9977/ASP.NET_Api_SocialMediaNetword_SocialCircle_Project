using Application;
using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateGroupMemberCommand : ICreateGroupMemberCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFCreateGroupMemberCommand(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 20;

        public string Name => "Creating group member using EF";

        public void Execute(CreateGroupMemberDto request)
        {
            

            var groupM = new GroupMember
            {
                GroupId = request.GroupId,
                UserId = _actor.Id,
                RoleId = 1
            };

            _context.GroupMembers.Add(groupM);
            _context.SaveChanges();
        }
    }
}
