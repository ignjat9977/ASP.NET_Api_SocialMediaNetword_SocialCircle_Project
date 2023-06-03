using Application.Commands;
using DataAcess;
using Domain;
using FluentValidation;
using ProjectNetworkMediaApi.Core.Validators;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreatePostCommand : ICreatePostCommand
    {


        private readonly MyDbContext _context;
        private readonly CreatePostValidator _postValidator;

        public EFCreatePostCommand(MyDbContext context, CreatePostValidator postValidator)
        {
            _context = context;
            _postValidator = postValidator;
        }

        public int Id => 7;

        public string Name => "Creating new Post using EF";

        public void Execute(PostDto request)
        {
            _postValidator.ValidateAndThrow(request);

            var post = new Post
            {
                Content = request.Content,
                Title = request.Title,
                PrivacyId = request.PrivacyId,
            };

            _context.Posts.Add(post);
            _context.UserWalls.Add(new UserWall
            {
                Post = post,
                UserId = request.UserId,
                Created = DateTime.UtcNow
            });
            _context.SaveChanges();
        }
    }
}
