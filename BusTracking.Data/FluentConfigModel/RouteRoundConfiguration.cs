using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class RouteRoundConfiguration : IEntityTypeConfiguration<RouteRound>
    {
        public void Configure(EntityTypeBuilder<RouteRound> builder)
        {
            builder.HasKey(x => new { x.RouteId, x.RoundId });
            builder.HasOne(x => x.Route)
                   .WithMany(r => r.RouteRounds)
                   .HasForeignKey(x => x.RouteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Round)
                   .WithMany(r => r.RouteRounds)
                   .HasForeignKey(x => x.RoundId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
