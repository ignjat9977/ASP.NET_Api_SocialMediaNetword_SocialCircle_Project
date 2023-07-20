using Application.Dto;
using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface ISearchAllNotificationsQuery : IQuery<SearchDto, PageResponse<NotificationDto>>
    {
    }
    public class NotificationDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public NotificationUserDto WhoMadeNotification { get; set; }
    }
    public class NotificationUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> ImagesPath { get; set; }
    }
}
