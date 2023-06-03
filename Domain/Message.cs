using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Message : Entity
    {
        public int SenderId { get; set; }
        public int ReciverId { get; set; }

        public User Sender { get; set; }
        public User Reciver { get; set; }
        public string Content { get; set; }
    }
}
