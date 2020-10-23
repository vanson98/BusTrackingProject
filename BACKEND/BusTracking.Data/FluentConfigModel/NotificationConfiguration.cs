using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Student)
                    .WithMany(s => s.Notifications)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.Content).IsRequired().HasMaxLength(500);
            builder.Property(n => n.TimeSent).IsRequired();
        }
    }
}
