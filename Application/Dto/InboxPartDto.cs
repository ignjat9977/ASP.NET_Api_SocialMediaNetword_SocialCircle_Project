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
        public int SenderId { get; set; }

        public string SenderName { get; set; }
        public string ReciverName { get; set; }
        public IEnumerable<MessDto> FirstThreeMessages { get; set; }
    }
    public class MessDto
    {
    
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
