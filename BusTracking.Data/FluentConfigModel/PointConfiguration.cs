using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.BusId).IsRequired().HasDefaultValue(-1);

            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(255);
            builder.Property(x => x.Longitude).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
        }
    }
}
