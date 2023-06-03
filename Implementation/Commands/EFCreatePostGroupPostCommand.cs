using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using ProjectNetworkMediaApi.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreatePostGroupPostCommand : ICreatePostGroupPostCommand
    {
        private readonly MyDbContext _context;
        private readonly CreatePostOrVideoInGroupValidator _validator;

        public EFCreatePostGroupPostCommand(MyDbContext context, CreatePostOrVideoInGroupValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 21;

        public string Name => "Creating Post in Group using EF";

        public void Execute(GroupPostDto request)
        {
            _validator.ValidateAndThrow(request);
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                PrivacyId = request.PrivacyId
            };
            var postG = new GroupPost
            {
                Post = post,
                UserId = request.UserId,
                GroupId = request.GroupId,
                Created = DateTime.UtcNow

            };
            _context.Posts.Add(post);
            _context.GroupPost.Add(postG);
            if (request.Path != null && request.VideoOrPhoto != null)
            {
                if(request.VideoOrPhoto == true)
                {
                    var photo = new PhotoVideo
                    {
                        Post = post,
                        Path = request.Path,
                        WhichFile = (bool)request.VideoOrPhoto

                    };
                    _context.PhotosVideos.Add(photo);
                }
                else
                {
                    var video = new PhotoVideo
                    {
                        Post = post,
                        Path = request.Path,
                        WhichFile = (bool)request.VideoOrPhoto

                    };
                    _context.PhotosVideos.Add(video);
                }
            }
            _context.SaveChanges();
        }
    }
}
