using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ReportDto
    {
        public int PostId { get; set; }
        public int ReportedUserId { get; set; }
        public string Description { get; set; }
    }
}
