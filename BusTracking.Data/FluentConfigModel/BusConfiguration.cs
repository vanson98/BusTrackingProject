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
                   .HasForeignKey<Driver>(d => d.BusId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Monitor)
                   .WithOne(m => m.Bus)
                   .HasForeignKey<Monitor>(m => m.BusId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Route)
                   .WithOne(r => r.Bus)
                   .HasForeignKey<Route>(r => r.BusId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Property(b => b.LicenseCode).IsRequired().HasMaxLength(12);
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.Status).IsRequired();

        }
    }
}
