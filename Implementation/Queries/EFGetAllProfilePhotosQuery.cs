using Application;
using Application.Dto;
using Application.Queries;
using DataAcess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class EFGetAllProfilePhotosQuery : IGetAllProfilePhotosQuery
    {
        private readonly IApplicationActor _actor;
        private readonly MyDbContext _context;

        public EFGetAllProfilePhotosQuery(IApplicationActor actor, MyDbContext context)
        {
            _actor = actor;
            _context = context;
        }

        public int Id => 21;

        public string Name => "Getting all profile photos using EF";

        public IEnumerable<ProfilePhotoDto> Execute(SearchDto request)
        {
            var photos = _context.UserProfilePhotos
                                .Include(x=>x.Photo)    
                                .Where(x => x.UserId == _actor.Id && (x.Photo.isActive || !x.Photo.isActive));


            return photos.Select(x => new ProfilePhotoDto
            {
                Id = x.Photo.Id,
                Created = x.Photo.CreatedAt,
                Path = x.Photo.Path,
            });
        }
    }
}
