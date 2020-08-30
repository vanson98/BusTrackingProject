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
            modelBuilder.Entity<Bus>().HasData(
                new Bus() {Id=1, LicenseCode="29S6-81655", Status=Status.Active},
                new Bus() {Id=2, LicenseCode="29S6-81656", Status=Status.Active},
                new Bus() {Id=3, LicenseCode="29S6-81657", Status=Status.Active }
            );

            // Driver
            modelBuilder.Entity<Driver>().HasData(
                new Driver() { 
                    Id=1,
                    FisrtName="Nguyễn", 
                    LastName="Sơn", 
                    Dob=new DateTime(2020,08,30),
                    Address="Định công", 
                    Email="abc@gmail.com", 
                    PhoneNumber="0364735051",
                    Status=Status.Active,
                    BusId=1
                },
                new Driver()
                {
                    Id = 2,
                    FisrtName = "Nguyễn",
                    LastName = "Hoàng",
                    Dob = new DateTime(1998, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735052",
                    Status = Status.Active,
                    BusId=2
                },
                new Driver()
                {
                    Id = 3,
                    FisrtName = "Nguyễn",
                    LastName = "Lâm",
                    Dob = new DateTime(1998, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735053",
                    Status= Status.Active,
                    BusId=3
                }
            );

            // Monitor
            modelBuilder.Entity<Monitor>().HasData(
                new Monitor()
                {
                    Id = 1,
                    FisrtName = "Nguyễn",
                    LastName = "Sơn",
                    Dob = new DateTime(2020, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735051",
                    Status = Status.Active,
                    BusId= 1,
                    AccountId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE")
                },
                new Monitor()
                {
                    Id = 2,
                    FisrtName = "Nguyễn",
                    LastName = "Hoàng",
                    Dob = new DateTime(1998, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735052",
                    Status = Status.Active,
                    AccountId = new Guid("DA5AC2AB-0346-416A-B640-D5915DAD85ED"),
                    BusId = 2,
                },
                new Monitor()
                {
                    Id = 3,
                    FisrtName = "Nguyễn",
                    LastName = "Lâm",
                    Dob = new DateTime(1998, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735053",
                    Status = Status.Active,
                    BusId = 3,
                    AccountId = new Guid("D5B139C2-3764-431F-900F-ECC01ADF5B91")
                }
            );
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var adminId1 = new Guid("DA5AC2AB-0346-416A-B640-D5915DAD85ED");
            var adminId2 = new Guid("D5B139C2-3764-431F-900F-ECC01ADF5B91");
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
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "vansonnguyen@gmail.com",
                    NormalizedEmail = "vansonnguyen@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123@"),
                    SecurityStamp = string.Empty,
                    FullName = "Nguyễn Văn Sơn",
                },
                new AppUser
                {
                    Id = adminId1,
                    UserName = "admin",
                    NormalizedUserName = "admin1",
                    Email = "vansonnguyen1@gmail.com",
                    NormalizedEmail = "vansonnguyen1@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123"),
                    SecurityStamp = string.Empty,
                    FullName = "Nguyễn Văn Trung",
                },
                new AppUser
                {
                    Id = adminId2,
                    UserName = "admin",
                    NormalizedUserName = "admin2",
                    Email = "vansonnguyen2@gmail.com",
                    NormalizedEmail = "vansonnguyen2@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123"),
                    SecurityStamp = string.Empty,
                    FullName = "Nguyễn Văn Lâm",
                }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
