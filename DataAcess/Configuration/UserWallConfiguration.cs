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
    public class UserWallConfiguration : IEntityTypeConfiguration<UserWall>
    {
        public void Configure(EntityTypeBuilder<UserWall> builder)
        {
            builder.HasOne(x=>x.User)
                .WithMany(x=>x.UserWalls)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.UserWalls)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
