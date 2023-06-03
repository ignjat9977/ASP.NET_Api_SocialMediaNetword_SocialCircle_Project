using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PostInesrtDto
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int PrivacyId { get; set; }
    }
}
