using Azure.Core;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reflection
{
    public static class FilterFunctions
    {
        
        private static IQueryable<Post> FilterByDateFrom(IQueryable<Post> entities, DateTime dateFrom)
        {
            return entities.Where(x => x.CreatedAt >= dateFrom);
        }
        private static IQueryable<Post> FilterByIsYourFriend(IQueryable<Post> entities, bool isOrNot)
        {
            if (!isOrNot)
            {
                return entities.Where(x => x.Privacy.Name == "public");

            }
            return entities;
        }
        private static IQueryable<Post> FilterByDateTo(IQueryable<Post> entities, DateTime dateTo)
        {
            return entities.Where(x => x.CreatedAt <= dateTo);
        }
        private static IQueryable<UseCaseLog> FilterByADateFrom(IQueryable<UseCaseLog> entities, DateTime dateFrom)
        {
            return entities.Where(x => x.CreatedAt >= dateFrom);
        }

        private static IQueryable<UseCaseLog> FilterByADateTo(IQueryable<UseCaseLog> entities, DateTime dateTo)
        {
            return entities.Where(x => x.CreatedAt <= dateTo);
        }
        private static IQueryable<Post> FilterByKeyword(IQueryable<Post> posts, string keyword)
        {
            return posts.Where(x => x.Title.Contains(keyword) || x.Content.Contains(keyword));
        }
        private static IQueryable<UseCaseLog> FilterByAKeyword(IQueryable<UseCaseLog> logs, string keyword)
        {
            return logs.Where(x => x.Actor.Contains(keyword) || x.UseCaseName.Contains(keyword));
        }

        private static IQueryable<Post> FilterByHasComments(IQueryable<Post> posts, bool hasComments)
        {
            return posts.Where(x => x.Comments.Any() == hasComments);
        }
        private static IQueryable<Post> FilterByUserId(IQueryable<Post> posts, int userId)
        {
            return posts.Where(x => x.UserWalls.Any(x => x.UserId == userId));
        }
        private static IQueryable<Post> FilterByGroupId(IQueryable<Post> posts, int groupId)
        {
            return posts.Where(x => x.GroupPosts.Any(x => x.GroupId == groupId));
        }
    }
}
