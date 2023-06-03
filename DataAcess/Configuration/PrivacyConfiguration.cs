using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Configuration
{
    public class PrivacyConfiguration : IEntityTypeConfiguration<Privacy>
    {
        public void Configure(EntityTypeBuilder<Privacy> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x=>x.Posts)
                .WithOne(x=>x.Privacy)
                .HasForeignKey(x=>x.PrivacyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
