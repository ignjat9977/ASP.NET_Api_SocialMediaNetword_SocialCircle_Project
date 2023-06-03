using Application.Dto;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface ISearchPostInGroupQuery : IQuery<SearchPostInGroupDto, PageResponse<PostDto>>
    {
    }
}
