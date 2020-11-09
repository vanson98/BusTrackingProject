using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.Extension
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Bus
           

            // Driver
           

           

            // Route
            
            // Identity
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var userId1 = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var userId2 = new Guid("DA5AC2AB-0346-416A-B640-D5915DAD85ED");
            var userId3 = new Guid("D5B139C2-3764-431F-900F-ECC01ADF5B91");

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = userId1,
                    UserName = "quan",
                    NormalizedUserName = "quan",
                    Email = "quan@gmail.com",
                    NormalizedEmail = "quan@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123@"),
                    SecurityStamp = string.Empty,
                    FullName = "Nguyễn Văn Quân",
                    Dob = new DateTime(1998,8,2),
                    TypeAccount = TypeAccount.WebAcc
                },
                new AppUser
                {
                    Id = userId2,
                    UserName = "tuan",
                    NormalizedUserName = "tuan",
                    Email = "tuan@gmail.com",
                    NormalizedEmail = "tuan@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123@"),
                    SecurityStamp = string.Empty,
                    FullName = "Nguyễn Văn Tuấn",
                },
                new AppUser
                {
                    Id = userId3,
                    UserName = "dung",
                    NormalizedUserName = "dung",
                    Email = "dung@gmail.com",
                    NormalizedEmail = "dung@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123@"),
                    SecurityStamp = string.Empty,
                    FullName = "Nguyễn Việt Dũng",
                }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = userId3
            });
        }
    }
}
