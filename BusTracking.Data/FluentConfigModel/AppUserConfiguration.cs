using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace BusTracking.Data.FluentConfigModel
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(255);
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.TypeAccount).IsRequired();
            builder.Property(u => u.Status).IsRequired().HasDefaultValue(Status.Active);
        }
    }
}
