using ProjectNetworkMediaApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class FriendAndFriends
    {
        public IEnumerable<UserDto> Friends { get; set; }
        public IEnumerable<UserDto> FriendsOf { get; set; }
    }
}
