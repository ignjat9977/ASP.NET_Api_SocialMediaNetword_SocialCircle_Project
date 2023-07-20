using Application;
using Application.Commands;
using Application.Exceptions;
using DataAcess;
using Domain;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFDeletePostCommand : IDeletePostCommand
    {

        private readonly MyDbContext _context;
        private readonly IApplicationActor _actor;

        public EFDeletePostCommand(MyDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 12;

        public string Name => "Delete post using Ef";

        public void Execute(int request)
        {
            var postToDelete = _context.Posts.Find(request);
            if(postToDelete == null)
            {
                throw new EntityNotFoundException(request, typeof(Post));
            }

            postToDelete.isActive = false;
            postToDelete.DeletedAt = DateTime.UtcNow;
            postToDelete.DeletedBy = _actor.Identity;
            _context.SaveChanges();


        }
    }
}
