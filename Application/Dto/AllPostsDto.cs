using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class AllPostsDto
    {
        public int? Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int PrivacyId { get; set; }
        public IEnumerable<string>? Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LikesCounter { get; set; }
        public int? CommentsCounter { get; set; }
        public IEnumerable<CommentDto>? Comments { get; set; }
        public IEnumerable<bool> WhichFile { get; set; }
        public UserNameDto? Name { get; set; }
    }
    public class UserNameDto
    {
        public string FirstName
        {
            get; set;
        }
        public string LastName { get; set; }
        public IEnumerable<string>? ImagesPath { get; set; }
    }
}
