using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CommentInsertDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public int? PostId { get; set; }
    }
}
