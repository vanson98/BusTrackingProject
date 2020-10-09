using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class StopConfiguration : IEntityTypeConfiguration<Stop>
    {
        public void Configure(EntityTypeBuilder<Stop> builder)
        {
            builder.HasKey(s=>s.Id);
            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Address).IsRequired();
            builder.Property(s => s.NumberOfStudents).IsRequired();
            builder.Property(s => s.TimePickUp).IsRequired();
            builder.Property(s => s.TimeDropOff).IsRequired();
            builder.Property(s => s.Longitude).HasColumnType("decimal(11,8)").IsRequired();
            builder.Property(s => s.Latitude).HasColumnType("decimal(10,8)").IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
        }
    }
}
