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
    public class EFSetProfilePhotoActiveCommand : ISetProfilePhotoActiveCommand
    {
        private readonly IApplicationActor _actor;
        private readonly MyDbContext _context;

        public EFSetProfilePhotoActiveCommand(IApplicationActor actor, MyDbContext context)
        {
            _actor = actor;
            _context = context;
        }
        public int Id => 24;

        public string Name => "Set profile photo active command using EF";

        public void Execute(int request)
        {
            var profilePhoto = _context.UserProfilePhotos.Where(x => x.UserId == _actor.Id).Select(x=>x.Photo);
            var pp = _context.PhotosVideos.Find(request);
            if (pp == null)
            {
                throw new EntityNotFoundException(request, typeof(PhotoVideo));
            }
            foreach (var photo in profilePhoto)
            {
                photo.isActive = false;
            }
            pp.isActive = true;

            _context.SaveChanges();
            
        }
    }
}
