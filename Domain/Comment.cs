using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment : Entity
    {
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
