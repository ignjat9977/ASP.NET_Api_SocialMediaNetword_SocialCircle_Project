
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public  ICollection<Like> Likes { get; set; } = new List<Like>();
        public  ICollection<Friend> Senters { get; set; } = new List<Friend>();
        public  ICollection<Friend> Recivers { get; set; } = new List<Friend>();
        public ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<UserProfilePhotos> UserProfilePhotos { get; set; } = new List<UserProfilePhotos>();
        public ICollection<UserWall> UserWalls { get; set; } = new List<UserWall>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Notification> ReciverNotifications { get; set; } = new List<Notification>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public ICollection<GroupPost> GroupPosts { get; set; } = new List<GroupPost>();
    }
}
