using Application;
using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using ProjectNetworkMediaApi.Core.Extensions;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFSearchGroupsQuery : ISearchGroupsQuery
    {
        private readonly MyDbContext _context;

        public EFSearchGroupsQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 20;

        public string Name => "Searching Groups using EF";

        public PageResponse<GroupDto> Execute(SearchDto request)
        {
            var groups = _context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                groups = groups.Where(x => x.Name.ToLower().Contains(request.Keyword.ToLower()));
            }
            groups = groups.Where(x => x.isActive);
            return groups.GetResult(request, x => new GroupDto
            {
                Id = x.Id,
                PrivacyId = x.PrivacyId,
                Name = x.Name,
                Description = x.Description
            });
        }
    }
}
