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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(1000);
        }
    }
}
