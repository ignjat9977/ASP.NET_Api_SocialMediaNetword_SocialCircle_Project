using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public ICollection<RoleUseCase> RoleUseCases { get; set; } = new List<RoleUseCase>();
    }
}
