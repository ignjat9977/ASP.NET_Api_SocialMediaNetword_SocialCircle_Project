using Application.Commands;
using Application.Dto;
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
    public class EFCreatePhotoOrVideoCommand : ICreatePhotoOrVideoCommand
    {

        private readonly MyDbContext _context;

        public EFCreatePhotoOrVideoCommand(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Creating picture using EF";

       

        public void Execute(UploadDto request)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                PrivacyId = request.PrivacyId
            };
            //true photo
            //false video
            var photo = new PhotoVideo
            {
                Post = post,
                Path = request.ImageFileName,
                WhichFile = request.PhotoOrVIdeo
                
            };

            var userWall = new UserWall
            {
                Post = post,
                UserId = request.UserId,
                Created = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            _context.PhotosVideos.Add(photo);
            _context.UserWalls.Add(userWall);

            _context.SaveChanges();
        }
    }
}
