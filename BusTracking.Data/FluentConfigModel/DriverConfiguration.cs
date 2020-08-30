using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).UseIdentityColumn();

            builder.Property(d => d.FisrtName).IsRequired();
            builder.Property(d => d.LastName).IsRequired();
            builder.Property(d => d.Address).HasMaxLength(255);
            builder.Property(d => d.Email).IsUnicode(false);
            builder.Property(d => d.Dob).IsRequired();
            builder.Property(d => d.PhoneNumber).IsRequired();
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.Status).IsRequired();
            builder.Property(d => d.BusId).IsRequired().HasDefaultValue(0);
        }
    }
}
