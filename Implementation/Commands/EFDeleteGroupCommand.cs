using Application;
using Application.Commands;
using Application.Exceptions;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFDeleteGroupCommand : IDeleteGroupCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFDeleteGroupCommand(MyDbContext context, IApplicationActor actor)
        {
            _actor = actor;
            _context = context;
        }

        public int Id => 11;

        public string Name => "Deleting group using EF";

        public void Execute(int request)
        {
            var group = _context.Groups.Find(request);

            if (group == null || !group.isActive)
                throw new EntityNotFoundException(request, typeof(Groupp));

            group.isActive = false;
            group.DeletedAt = DateTime.UtcNow;
            group.DeletedBy = _actor.Identity;

            _context.SaveChanges();
        }
    }
}
