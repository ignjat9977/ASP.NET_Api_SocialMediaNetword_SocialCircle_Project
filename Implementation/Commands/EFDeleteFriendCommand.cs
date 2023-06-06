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
    public class EFDeleteFriendCommand : IDeleteFriendCommand
    {
        private readonly MyDbContext _context;
        private readonly IApplicationActor _user;

        public EFDeleteFriendCommand(MyDbContext context, IApplicationActor user)
        {
            _context = context;
            _user = user;
        }

        public int Id => 10;

        public string Name => "Deleting friend using EF";

        public void Execute(int request)
        {
            
            var friendshipToDelete = _context.Friends.Where(x => x.UserId == _user.Id && x.FriendId == request).FirstOrDefault();

            if(friendshipToDelete == null)
            {
                friendshipToDelete = _context.Friends.Where(x=>x.UserId == request && x.FriendId == _user.Id).FirstOrDefault();
                if(friendshipToDelete == null)
                {
                    throw new EntityNotFoundException(request, typeof(Friend));
                }
            }

            _context.Friends.Remove(friendshipToDelete);
            _context.SaveChanges();


        }
    }
}
