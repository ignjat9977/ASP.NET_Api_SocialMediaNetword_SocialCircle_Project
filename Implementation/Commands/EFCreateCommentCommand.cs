using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.AspNetCore.SignalR;
using ProjectNetworkMediaApi.SRHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateCommentCommand : ICreateCommentCommand
    {
        private readonly MyDbContext _context;
        private readonly CreateCommentValidator _validator;

        private readonly ConnectionMapping _connectionMapping;
        private readonly IHubContext<ChatHub> _hubContext;

        public EFCreateCommentCommand(MyDbContext context, CreateCommentValidator validator, ConnectionMapping connectionMapping, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _validator = validator;
            _connectionMapping = connectionMapping;
            _hubContext = hubContext;
        }

        public int Id => 1;

        public string Name => "Creating Comment using EF";

        public async void Execute(CommentInsertDto request)
        {
            _validator.ValidateAndThrow(request);

            var comment = new Comment
            {
                UserId = request.UserId,
                PostId = request.PostId,
                ParentId = request.ParentId,
                Content = request.Content
            };
            var sender = _context.Users.Find(request.UserId);
            int reciverId = 0;
            if(request.ParentId != null)
            {
                var parentComment = _context.Comments.Find(request.ParentId);
                reciverId = (int)parentComment.UserId;

                await SendNotification(reciverId.ToString(), sender, request.Content);

            }
            else
            {
                var post = _context.Posts.Find(request.PostId);
                var userWall = _context.UserWalls.Where(x => x.PostId == request.PostId).Select(x => x.User.Id);
                var groupWall = _context.GroupPost.Where(x => x.PostId == request.PostId).Select(x => x.User.Id);
                if(userWall.Any())
                {
                    reciverId = userWall.First();
                }

                if (groupWall.Any())
                {
                    reciverId = groupWall.First();
                }
                

                await SendNotification(reciverId.ToString(), sender, request.Content);
            }
            Notification not = new Notification
            {
                ReciverId = reciverId,
                UserId = sender.Id,
                Description = $"{sender.FirstName} {sender.LastName} Repost you with: {request.Content}"
            };

            _context.Comments.Add(comment);
            _context.Notifications.Add(not);
            _context.SaveChanges();
        }
        public async Task SendNotification(string reciverId, User sender, string desciption)
        {
            var connectionId = _connectionMapping.GetConnectionForUser(reciverId);

            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", $"{sender.FirstName} {sender.LastName}", $"Repost you with: {desciption}");
            }
        }
    }
}
