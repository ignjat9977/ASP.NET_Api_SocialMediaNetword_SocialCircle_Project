﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ProfilePhotoDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime Created { get; set; }
    }
}
