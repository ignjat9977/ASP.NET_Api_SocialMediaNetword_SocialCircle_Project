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

        public EFDeleteFriendCommand(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 10;

        public string Name => "Deleting friend using EF";

        public void Execute(int request)
        {
            var friendToDelete = _context.Friends.Find(request);

            if(friendToDelete == null)
            {
                throw new EntityNotFoundException(request, typeof(Friend));
            }

            _context.Friends.Remove(friendToDelete);
            _context.SaveChanges();


        }
    }
}
