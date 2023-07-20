using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class SpecificMessagesDto
    {
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string SenderName { get; set; }
        public string ReciverName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
