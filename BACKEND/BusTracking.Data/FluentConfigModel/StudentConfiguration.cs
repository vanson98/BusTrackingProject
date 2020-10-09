using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.FluentConfigModel
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).UseIdentityColumn();

            builder.HasOne(s => s.Bus)
                   .WithMany(b => b.Students)
                   .HasForeignKey(s => s.BusId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s => s.BusId).IsRequired(true);

            builder.HasOne(s => s.Parent)
                   .WithMany(p => p.Students)
                   .HasForeignKey(s => s.ParentId);
            builder.Property(s => s.ParentId).IsRequired(true);

            builder.HasOne(x => x.Stop)
                   .WithMany(s => s.Students)
                   .HasForeignKey(x => x.StopId);
            builder.Property(s => s.StopId).IsRequired(false);

            builder.Property(s => s.TypeTransport).IsRequired();
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.Address).HasMaxLength(255);
            builder.Property(d => d.Email).IsUnicode(false);
            builder.Property(d => d.Dob).IsRequired();
            builder.Property(d => d.PhoneNumber).IsRequired();
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.Status).IsRequired();
        }
    }
}
