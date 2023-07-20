using Application.Commands;
using Application.Dto;
using Azure.Core;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.SignalR;
using ProjectNetworkMediaApi.SRHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class EFCreateLikeCommentCommand : ICreateLikeCommentCommand
    {
        private readonly MyDbContext _context;
        private readonly ConnectionMapping _connectionMapping;
        private readonly IHubContext<ChatHub> _hubContext;

        public EFCreateLikeCommentCommand(MyDbContext context,ConnectionMapping connectionMapping,IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _connectionMapping = connectionMapping;
            _hubContext = hubContext;
        }

        public int Id =>4;

        public string Name => "Like Comment using EF";

        public async void Execute(CommentLikeDto request)
        {
            Like like = new Like
            {
                PostId = null,
                UserId = request.UserId,
                CommentId = request.CommentId
            };

            var likeExist = _context.Likes
                .FirstOrDefault(l => l.CommentId == request.CommentId &&
                                      l.UserId == request.UserId);



            //ako lajk ne postoji znaci upisujemo like u tabelu
            if (likeExist == null)
            {
                //nalazimo koji korisnik je napisao komentar i njega obavestavamo da mu je lajkovan komentar
                var comment = _context.Comments.Find(request.CommentId);
                var userId = comment.UserId;
                string commentDescription = comment.Content;
                //proveravamo da korisnik nije sam sebi lajkovao komentar
                if (request.UserId != userId)
                {
                    User sender = _context.Users.Find(request.UserId);

                    await SendNotification(userId.ToString(), sender, commentDescription);
                    

                    //na kraju upisujemo notifikaciju u bazu podataka

                    Notification not = new Notification
                    {
                        ReciverId = (int)userId,
                        UserId = sender.Id,
                        Description = $"{sender.FirstName} {sender.LastName}, liked your Comment",
                    };
                    _context.Notifications.Add(not);
                }

                _context.Likes.Add(like);
            }
            else
            {
                _context.Likes.Remove(likeExist);
            }
                


            _context.SaveChanges();
        }
        public async Task SendNotification(string reciverId, User sender, string commentDescription)
        {
            var connectionId = _connectionMapping.GetConnectionForUser(reciverId);

            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", $"{sender.FirstName} {sender.LastName}", $"Liked your comment: {commentDescription}");
            }
        }
    }
}
