using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PhotoVideo : Entity
    {
        public string Path { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
        public bool WhichFile { get; set; }
        public ICollection<UserProfilePhotos> UserProfilePhotos { get; set; } = new List<UserProfilePhotos>();
    }
}
