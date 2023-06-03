using Application.Commands;
using Application.Dto;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.SignalR;
using ProjectNetworkMediaApi.SRHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateLikePostCommand : ICreateLikePostCommand
    {

        private readonly MyDbContext _context;
        private readonly ConnectionMapping _connectionMapping;
        private readonly IHubContext<ChatHub> _hubContext;

        public EFCreateLikePostCommand(MyDbContext context, ConnectionMapping connectionMapping, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _connectionMapping = connectionMapping;
            _hubContext = hubContext;
        }

        public int Id => 5;

        public string Name => "Like Post using EF";

        public async void Execute(PostLikeDto request)
        {
            Like like = new Like
            {
                CommentId = null,
                PostId = request.PostId,
                UserId = request.UserId,
            };

            var likeExist = _context.Likes
                .FirstOrDefault(l => l.PostId == request.PostId &&
                                    l.UserId == request.UserId);

            
            if (likeExist == null)
            {
                var post = _context.Posts.Find(request.PostId);
                //korisnik kome treba da se posalje notifikacija
                var userIds = _context.UserWalls.Where(x=>x.PostId == request.PostId).Select(x=>x.UserId) != null ?
                             _context.UserWalls.Where(x => x.PostId == request.PostId).Select(x => x.UserId) :
                             _context.GroupPost.Where(x => x.PostId == request.PostId).Select(x => x.UserId);

                var userId = userIds.FirstOrDefault();
                string postDescription = post.Title;
                //proveravamo da korisnik nije sam sebi lajkovao post
                if (request.UserId != userId)
                {
                    User sender = _context.Users.Find(request.UserId);
                    await SendNotification(userId.ToString(), sender, postDescription);

                    Notification not = new Notification
                    {
                        IsRead = false,
                        UserId = userId,
                        Description = $"{sender.FirstName} {sender.LastName}, liked your Post",
                    };
                    _context.Notifications.Add(not);
                }
                _context.Likes.Add(like);
            } 
            else
                _context.Likes.Remove(likeExist);

            _context.SaveChanges();
        }
        public async Task SendNotification(string reciverId, User sender, string postDescription)
        {
            var connectionId = _connectionMapping.GetConnectionForUser(reciverId);

            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", $"{sender.FirstName} {sender.LastName}", $"Liked your post: {postDescription}");
            }
        }
    }
}
