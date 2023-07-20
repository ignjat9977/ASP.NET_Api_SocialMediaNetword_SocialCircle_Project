using Application.Dto;
using Application.Exceptions;
using Application.Queries;
using DataAcess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetGroupInfoQuery : IGetGroupInfoQuery
    {
        private readonly MyDbContext _context;

        public EFGetGroupInfoQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "See Group Info using EF";

        public GroupDto Execute(int request)
        {
            var group = _context.Groups.Find(request);

            if (group == null || !group.isActive)
                throw new EntityNotFoundException(request, typeof(Groupp));

            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CreatedAt = group.CreatedAt,
                PrivacyId = group.PrivacyId
            };
        }
    }
}
