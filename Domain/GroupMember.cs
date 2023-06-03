﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GroupMember 
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Groupp Group { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
