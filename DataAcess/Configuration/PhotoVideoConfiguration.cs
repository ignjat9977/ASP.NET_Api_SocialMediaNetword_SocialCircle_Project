using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configuration
{
    public class PhotoVideoConfiguration : IEntityTypeConfiguration<PhotoVideo>
    {
        public void Configure(EntityTypeBuilder<PhotoVideo> builder)
        {
            builder.Property(x => x.Path).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PostId).IsRequired(false);


            builder.HasOne(x => x.Post)
                .WithMany(x => x.PhotosVideos)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UserProfilePhotos)
               .WithOne(x => x.Photo)
               .HasForeignKey(x => x.PhotoId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
