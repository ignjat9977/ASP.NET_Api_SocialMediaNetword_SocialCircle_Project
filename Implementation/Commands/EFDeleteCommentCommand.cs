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
    public class EFDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly MyDbContext _context;

        public EFDeleteCommentCommand(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 9;

        public string Name => "Delete Comment using EF";

        public void Execute(int request)
        {
            var comment = _context.Comments.Find(request);

            if (comment == null)
                throw new EntityNotFoundException(request, typeof(Comment));

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
