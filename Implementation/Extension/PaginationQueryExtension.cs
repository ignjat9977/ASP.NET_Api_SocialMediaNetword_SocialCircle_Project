using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Extension
{
   public static class PaginationQueryExtension
{
    public static PageResponse<TDto> GetResult<T, TDto>(this IQueryable<T> entities,
        PageSearch search,
        Expression<Func<T, TDto>> expression)
        where TDto : class
        where T : class
    {
        if (search.PerPage <= 0)
        {
            search.PerPage = 10;
        }
        if (search.Page <= 0)
        {
            search.Page = 1;
        }

        var skipCount = search.PerPage * (search.Page - 1);
        var totalCount = entities.Count();

        var items = entities
            .Skip(skipCount)
            .Take(search.PerPage)

            .Select(expression)
            .ToList();

        var response = new PageResponse<TDto>
        {
            CurrentPage = search.Page,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            Items = items
        };

        return response;
    }
}

}
