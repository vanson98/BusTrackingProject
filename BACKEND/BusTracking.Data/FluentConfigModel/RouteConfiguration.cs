using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn();

            builder.Property(r => r.RouteCode).IsRequired().IsUnicode(false);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Distance).IsRequired();
            builder.Property(r => r.TimePickUp).IsRequired();
            builder.Property(r => r.TimeDropOff).IsRequired();
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);
            builder.Property(r => r.Status).IsRequired();
        }
    }
}
