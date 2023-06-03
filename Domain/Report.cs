using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Report : Entity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int ReportedUserId { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public User ReportedUser { get; set; }
        public Post Post { get; set; }
    }
}
