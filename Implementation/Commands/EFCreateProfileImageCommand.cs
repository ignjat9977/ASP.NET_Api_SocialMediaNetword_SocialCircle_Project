using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateProfileImageCommand : ICreateProfileImageCommand
    {
        private readonly MyDbContext _context;

        public EFCreateProfileImageCommand(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 22;

        public string Name => "Creating profile image using EF";

        public void Execute(UploadDto request)
        {
            var photo = new PhotoVideo
            {
                WhichFile = true,
                Path = request.ImageFileName,
                PostId = null
            };

            var userProfileImage = new UserProfilePhotos
            {
                Photo = photo,
                UserId = request.UserId,
                Created = DateTime.Now
            };

            _context.PhotosVideos.Add(photo);
            _context.UserProfilePhotos.Add(userProfileImage);
            _context.SaveChanges();
        }
    }
}
