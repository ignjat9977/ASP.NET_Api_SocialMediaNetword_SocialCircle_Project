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
    public class EFDeleteUserCommand : IDeleteUserCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFDeleteUserCommand(MyDbContext context,IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 30;

        public string Name => "Deleting User using EF";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if(user == null || !user.isActive)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }

            user.isActive = false;
            user.DeletedAt = DateTime.UtcNow;
            user.DeletedBy = _actor.Identity;

            _context.SaveChanges();
        }
    }
}
