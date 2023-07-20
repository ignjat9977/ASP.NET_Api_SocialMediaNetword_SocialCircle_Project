using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFSearchReportsQuery : ISearchReportsQuery
    {
        private readonly MyDbContext _context;

        public EFSearchReportsQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 33;

        public string Name => "Searching reports using EF";

        public PageResponse<ReportsSDto> Execute(SearchReportsDto request)
        {
            var reports = _context.Reports.AsQueryable();


            if(request.Keyword != null)
            {
                reports = reports.Where(x=> x.Description.ToLower().Contains(request.Keyword.ToLower()));  
            }
            if(request.UserId != null)
            {
                reports = reports.Where(x=> x.UserId == request.UserId || x.ReportedUserId == request.UserId);
            }


            return reports.GetResult(request, x => new ReportsSDto
            {
                Id = x.Id,
                Created = x.CreatedAt,
                Description = x.Description,
                PostId = x.PostId,
                ReportedUserId = x.ReportedUserId,
                UserId = x.UserId,
            });
        }
    }
}
