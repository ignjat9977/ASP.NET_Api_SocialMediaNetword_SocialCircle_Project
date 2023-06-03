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

        public EFDeleteGroupCommand(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 11;

        public string Name => "Deleting group using EF";

        public void Execute(int request)
        {
            var group = _context.Groups.Find(request);

            if (group == null)
                throw new EntityNotFoundException(request, typeof(Groupp));

            _context.Groups.Remove(group);
            _context.SaveChanges();
        }
    }
}
