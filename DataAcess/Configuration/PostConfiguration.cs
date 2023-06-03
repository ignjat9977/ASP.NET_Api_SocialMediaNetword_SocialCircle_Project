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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Content).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
           
            builder.HasMany(x=>x.Reports)
                .WithOne(x=>x.Post)
                .HasForeignKey(x=>x.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
