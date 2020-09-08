using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class BusConfiguration : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Driver)
                   .WithOne(d => d.Bus)
                   .HasForeignKey<Bus>(x => x.DriverId)
                   .OnDelete(DeleteBehavior.SetNull);
                   

            builder.HasOne(b => b.Monitor)
                   .WithOne(m => m.Bus)
                   .HasForeignKey<Bus>(b => b.MonitorId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Route)
                   .WithOne(r => r.Bus)
                   .HasForeignKey<Bus>(b => b.RouteId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(b => b.DriverId).IsRequired(false);
            builder.Property(b => b.MonitorId).IsRequired(false);
            builder.Property(b => b.RouteId).IsRequired(false);
            builder.Property(b => b.LicenseCode).IsRequired().HasMaxLength(12);
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.Status).IsRequired();
            builder.Property(b => b.MaxSize).IsRequired();

        }
    }
}
