using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notification : Entity
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsRead { get; set; }
    }
}
