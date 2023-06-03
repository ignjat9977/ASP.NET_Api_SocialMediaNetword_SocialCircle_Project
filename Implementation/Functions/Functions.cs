using Application.Dto;
using DataAcess;
using Domain;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Dto;
using ProjectNetworkMediaApi.SRHub;

namespace ProjectNetworkMediaApi.Core.Extensions
{
    public static class Functions
    {
        
        public static IEnumerable<CommentDto> BuildNestedComments(this ICollection<Comment> comments, int? parentId = null)
        {
            comments = comments.ToList();

            var nestedComments = comments
                .Where(c => c.ParentId == parentId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    PostId = c.PostId,
                    UserFirstAndLast = $"{c.User.FirstName} {c.User.LastName}",
                    UserImagesPaths= c.User.UserProfilePhotos.Select(x=>x.Photo.Path),
                    ParentId = c.ParentId,
                    LikesCounter = c.Likes.Count(),
                    CreatedAt = c.CreatedAt,
                    Comments = c.ChildComments.BuildNestedComments(c.Id)
                })
                .ToList();

            return nestedComments;
        }

    }
}
