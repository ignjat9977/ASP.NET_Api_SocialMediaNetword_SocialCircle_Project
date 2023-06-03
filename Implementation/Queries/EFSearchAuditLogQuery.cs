using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Implementation.Extension;
using Implementation.Reflection;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFSearchAuditLogQuery : ISearchAuditLogQuery
    {
        private readonly MyDbContext _context;

        public EFSearchAuditLogQuery(MyDbContext context)
        {
            _context = context;
        }

        public int Id => 29;

        public string Name => "Searching audit log using EF";

        public PageResponse<UseCaseLogDto> Execute(SearchAuditLogDto request)
        {
            var log = _context.UseCaseLogs.AsQueryable();



            var properties = typeof(SearchAuditLogDto).GetProperties();

            log = log.StartFiltering<UseCaseLog>(properties, request);

            return log.GetResult(request, x => new UseCaseLogDto
            {
                Id = x.Id,
                Actor = x.Actor,
                Data = x.Data,
                Date = x.CreatedAt,
                UseCaseName = x.UseCaseName
            });
            //var skipCount = request.PerPage * (request.Page - 1);
            //var response = new PageResponse<UseCaseLogDto>
            //{
            //    CurrentPage = request.Page,
            //    ItemsPerPage = request.PerPage,
            //    TotalCount = log.Count(),
            //    Items = log.Skip(skipCount).Take(request.PerPage).Select(x => new UseCaseLogDto
            //    {
            //        Id = x.Id,
            //        Actor = x.Actor,
            //        Data = x.Data,
            //        Date = x.CreatedAt,
            //        UseCaseName = x.UseCaseName

            //    }).ToList()

            //};
            //return response;
        }
    }
}
