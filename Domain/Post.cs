using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Post : Entity
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int PrivacyId { get; set; }
        public Privacy Privacy { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<GroupPost> GroupPosts { get; set; } = new List<GroupPost>();
        public ICollection<PhotoVideo> PhotosVideos { get; set; } = new List<PhotoVideo>();
        public ICollection<UserWall> UserWalls { get; set; } = new List<UserWall>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
