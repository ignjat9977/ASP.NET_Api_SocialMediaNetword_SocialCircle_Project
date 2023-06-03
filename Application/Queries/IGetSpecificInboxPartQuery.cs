using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IGetSpecificInboxPartQuery : IQuery<int, SpecificMessagesDto>
    {
    }
}
