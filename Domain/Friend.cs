using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Friend : Entity
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public User User { get; set; }
        public User OneFriend { get; set; }

    }
}
