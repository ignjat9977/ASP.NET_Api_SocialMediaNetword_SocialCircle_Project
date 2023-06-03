using Application;
using Application.Commands;
using Application.Exceptions;
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
    public class EFUpdatePostCommand : IUpdatePostCommand
    {
        private MyDbContext _context;
        private UpdatePostValidator _validator;
        private IApplicationActor actor;

        public EFUpdatePostCommand(MyDbContext context, UpdatePostValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            this.actor = actor;
        }

        public int Id => 14;

        public string Name => "EF Command for update post";

        public void Execute(PostDto request)
        {
            _validator.ValidateAndThrow(request);

            var post = _context.Posts.Find(request.Id);
            if(post == null)
            {
                throw new EntityNotFoundException((int)request.Id, typeof(Post));
            }

            post.Content = request.Content;
            post.Title = request.Title;
            post.PrivacyId = request.PrivacyId;
            
            _context.SaveChanges();
        }
    }
}
