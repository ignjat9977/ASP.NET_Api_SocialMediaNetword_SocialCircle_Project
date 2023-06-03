﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserProfilePhotos
    {
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public User User { get; set; }  
        public PhotoVideo Photo { get; set; }
        public DateTime Created { get; set; }

    }
}
