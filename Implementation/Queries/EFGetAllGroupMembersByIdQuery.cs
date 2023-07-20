using Application.Queries;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetAllGroupMembersByIdQuery : IGetAllGroupMembersByIdQuery
    {
        private readonly MyDbContext _context;

        public EFGetAllGroupMembersByIdQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 22;

        public string Name => "Get Group Members By id using EF";

        public IEnumerable<int> Execute(int request)
        {
            var ids = _context.GroupMembers.Where(x=>x.GroupId == request).Select(x => x.UserId);

            return ids;
        }
    }
}
