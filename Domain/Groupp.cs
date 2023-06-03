using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Groupp : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int PrivacyId { get; set; }

        public Privacy Privacy { get; set; }
        public ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public ICollection<GroupPost> GroupPosts { get; set; } = new List<GroupPost>();
    }
}
