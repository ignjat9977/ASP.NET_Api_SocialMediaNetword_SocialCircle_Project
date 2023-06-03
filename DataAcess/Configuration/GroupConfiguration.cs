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
    public class GroupConfiguration : IEntityTypeConfiguration<Groupp>
    {
        public void Configure(EntityTypeBuilder<Groupp> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);

            builder.HasOne(x => x.Privacy)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.PrivacyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x=>x.GroupMembers)
                .WithOne(x=>x.Group)
                .HasForeignKey(x=>x.GroupId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
