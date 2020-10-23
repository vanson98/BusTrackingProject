using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class StudentCheckInConfiguration : IEntityTypeConfiguration<StudentCheckIn>
    {
        public void Configure(EntityTypeBuilder<StudentCheckIn> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Student)
                .WithMany(s => s.StudentCheckIns)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Monitor)
                .WithMany(s => s.StudentCheckIns)
                .HasForeignKey(x => x.MonitorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Stop)
                .WithMany(s => s.StudentCheckIns)
                .HasForeignKey(x => x.StopId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.CheckInTime).IsRequired();
            builder.Property(x => x.CheckInResult).IsRequired();
            builder.Property(x => x.StudentId).IsRequired();
        }
    }
}
