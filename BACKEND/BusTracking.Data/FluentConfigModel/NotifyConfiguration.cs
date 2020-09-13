using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class NotifyConfiguration : IEntityTypeConfiguration<Notify>
    {
        public void Configure(EntityTypeBuilder<Notify> builder)
        {
            builder.HasNoKey();
            builder.Property(n => n.Content).IsRequired().HasMaxLength(500);
            builder.Property(n => n.TimeSent).IsRequired();
        }
    }
}
