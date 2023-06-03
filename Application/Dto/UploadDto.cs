using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto

{
    public class UploadDto
    {
        public string ImageFileName { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public int PrivacyId { get; set; }
        public bool PhotoOrVIdeo { get; set; }
    }
}
