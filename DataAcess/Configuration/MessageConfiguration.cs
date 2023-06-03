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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Content).HasMaxLength(500);
            builder.HasOne(x => x.Reciver)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ReciverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
