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
    public class EFDeleteProfilePhotoCommand : IDeleteProfilePhotoCommand
    {
        private readonly IApplicationActor _actor;
        private readonly MyDbContext _context;

        public EFDeleteProfilePhotoCommand(IApplicationActor actor, MyDbContext context)
        {
            _actor = actor;
            _context = context;
        }
        public int Id => 20;

        public string Name => "Deleting profile photo using EF";

        public void Execute(int request)
        {
            var pp = _context.PhotosVideos.Find(request);

            if(pp == null)
            {
                throw new EntityNotFoundException(request, typeof(PhotoVideo));
            }
            var relateTable = _context.UserProfilePhotos.Where(x => x.PhotoId == request);
            _context.UserProfilePhotos.RemoveRange(relateTable);
            _context.PhotosVideos.Remove(pp);
            _context.SaveChanges();
        }
    }
}
