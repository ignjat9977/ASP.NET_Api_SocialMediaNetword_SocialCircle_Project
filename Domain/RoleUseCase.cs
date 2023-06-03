using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RoleUseCase
    {
        public int RoleUseCaseId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
