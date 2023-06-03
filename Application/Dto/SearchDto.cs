using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class SearchDto : PageSearch
    {
        public string? Keyword { get; set; }
        
    }
}
