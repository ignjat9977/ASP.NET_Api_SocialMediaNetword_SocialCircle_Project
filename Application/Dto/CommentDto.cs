﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
        public int? PostId { get; set; }

        public string UserFirstAndLast { get; set; }
        public IEnumerable<string> UserImagesPaths { get; set; }

        public int LikesCounter { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
