using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class SearchAuditLogDto: PageSearch
    {
        public string? AKeyword { get; set; }
        public DateTime? ADateFrom { get; set; }
        public DateTime? ADateTo { get; set; }
    }
}
