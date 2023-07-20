using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class InboxPartDto
    {
        public int ReciverId { get; set; }
        public int IdUserNameWhoITextWith { get; set; }

        public string UserNameWhoITextWith { get; set; }
    }
    public class MessDto
    {
        public string Sender { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
