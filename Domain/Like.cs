using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Like : Entity
    {
        public int? PostId { get; set; } = null;
        public int? CommentId { get; set; } = null;
        public int UserId { get; set; }

        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
