

using Application.Dto;
using System.Xml.Linq;

namespace ProjectNetworkMediaApi.Dto
{
    public class PostDto 
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
        public IEnumerable<bool>? WhichFile { get; set; }
    }
}
