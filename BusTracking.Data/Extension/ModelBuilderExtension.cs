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
                new Bus() {
                    Id=1, 
                    LicenseCode="29S6-81655",
                    MaxSize=12,MaxSpeed=45,
                    Description=null, 
                    Status=Status.Active,
                    DriverId = 1,
                    MonitorId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    RouteId = 1
                },
                new Bus() {
                    Id=2, 
                    LicenseCode="29S6-81656",
                    MaxSize = 10,
                    MaxSpeed = 40,
                    Description = null,
                    Status = Status.Active,
                    DriverId = 2,
                    MonitorId = new Guid("DA5AC2AB-0346-416A-B640-D5915DAD85ED"),
                    RouteId = 2
                },
                new Bus() {
                    Id=3, 
                    LicenseCode="29S6-81657",
                    MaxSize = 22,
                    MaxSpeed = 35,
                    Description = null,
                    Status = Status.Active,
                    DriverId = 3,
                    MonitorId = new Guid("D5B139C2-3764-431F-900F-ECC01ADF5B91"),
                    RouteId = 3
                }
            );

            // Driver
            modelBuilder.Entity<Driver>().HasData(
                new Driver() { 
                    Id=1,
                    Name="Nguyễn Sơn", 
                    Dob=new DateTime(2020,08,30),
                    Address="Định công", 
                    Email="abc@gmail.com", 
                    PhoneNumber="0364735051",
                    Status=Status.Active,
                },
                new Driver()
                {
                    Id = 2,
                    Name = "Nguyễn Hoàng",
                    Dob = new DateTime(1998, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735052",
                    Status = Status.Active
                },
                new Driver()
                {
                    Id = 3,
                    Name = "Nguyễn Minh Đức",
                    Dob = new DateTime(1998, 08, 30),
                    Address = "Định công",
                    Email = "abc@gmail.com",
                    PhoneNumber = "0364735053",
                    Status= Status.Active
                }
            );

           

            // Route
            modelBuilder.Entity<Route>().HasData(
                new Route() { Id = 1,RouteCode="R001",Name="Tuyến 01",Distance=(decimal)12.34,Status=Status.Active},
                new Route() { Id = 2,RouteCode="R002",Name="Tuyến 02",Distance=(decimal)10.34,Status=Status.Active},
                new Route() { Id = 3,RouteCode="R003",Name="Tuyến 03",Distance=(decimal)77.15,Status=Status.Active}
            );
            // Identity
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
                    Dob = new DateTime(1998,8,2),
                    TypeAccount = TypeAccount.WebAcc
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
