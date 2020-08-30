using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class RouteStopConfiguration : IEntityTypeConfiguration<RouteStop>
    {
        public void Configure(EntityTypeBuilder<RouteStop> builder)
        {
            builder.HasKey(rs => new { rs.RouteId, rs.StopId });
            builder.HasOne(rs => rs.Route)
                   .WithMany(r => r.RouteStops)
                   .HasForeignKey(rs => rs.RouteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rs => rs.Stop)
                   .WithMany(s => s.RouteStops)
                   .HasForeignKey(rs => rs.StopId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
