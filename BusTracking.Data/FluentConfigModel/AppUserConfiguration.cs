using BusTracking.Data.Entities;
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

            builder.HasOne(x => x.Monitor)
                   .WithOne(m => m.Account)
                   .HasForeignKey<Monitor>(m => m.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.TypeAccount).IsRequired();
            builder.Property(u => u.Status).IsRequired().HasDefaultValue(1);
        }
    }
}
