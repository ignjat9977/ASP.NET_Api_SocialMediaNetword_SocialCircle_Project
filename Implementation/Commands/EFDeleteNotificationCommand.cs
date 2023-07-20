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
    public class EFDeleteNotificationCommand : IDeleteNotificationCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFDeleteNotificationCommand(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 26;

        public string Name => "Deleting notification using EF";

        public void Execute(int request)
        {
            var notification = _context.Notifications.Find(request);

            if(notification == null || !notification.isActive)
            {
                throw new EntityNotFoundException(request, typeof(Notification));
            }

            notification.isActive = false;
            notification.DeletedAt = DateTime.UtcNow;
            notification.DeletedBy = _actor.Identity;

            _context.SaveChanges(); 
        }
    }
}
