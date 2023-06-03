using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Privacy : Entity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Groupp> Groups { get; set; } = new List<Groupp>();
    }
}
