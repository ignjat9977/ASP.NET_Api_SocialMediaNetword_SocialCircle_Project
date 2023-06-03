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
    public class GroupPostConfiguration : IEntityTypeConfiguration<GroupPost>
    {
        public void Configure(EntityTypeBuilder<GroupPost> builder)
        {
            builder.HasOne(x=>x.Post)
                .WithMany(x=>x.GroupPosts)
                .HasForeignKey(x=>x.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Group)
                .WithMany(x => x.GroupPosts)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
