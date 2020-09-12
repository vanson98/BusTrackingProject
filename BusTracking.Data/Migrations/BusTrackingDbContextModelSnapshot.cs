﻿// <auto-generated />
using System;
using BusTracking.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusTracking.Data.Migrations
{
    [DbContext(typeof(BusTrackingDbContext))]
    partial class BusTrackingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusTracking.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                            ConcurrencyStamp = "736cbcfa-d67a-4793-ab63-378514084dea",
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("BusTracking.Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("TypeAccount")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4299a979-580a-4f88-be9b-86fd2f5325ec",
                            Dob = new DateTime(1998, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vansonnguyen@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Nguyễn Văn Sơn",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "vansonnguyen@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEOJonMmNDkov4Z5Y08g2ARhznZ5nzguE+tKUkgG+9bEqCs1q/OajQc3CQc3+BbTqiA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = 0,
                            TwoFactorEnabled = false,
                            TypeAccount = 2,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0c20a976-af7a-4d4c-95b5-e88af4fe9f86",
                            Dob = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vansonnguyen1@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Nguyễn Văn Trung",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "vansonnguyen1@gmail.com",
                            NormalizedUserName = "admin1",
                            PasswordHash = "AQAAAAEAACcQAAAAEFl47s1As0DwKhfKjJZtJa8E4ARDkdQDJvcSD2fx0yRg61zwBzZJZ7Pclg7/b6M2DQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = 0,
                            TwoFactorEnabled = false,
                            TypeAccount = 0,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b432f0d1-eb3f-4274-bff8-5d9d4c15138a",
                            Dob = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vansonnguyen2@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Nguyễn Văn Lâm",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "vansonnguyen2@gmail.com",
                            NormalizedUserName = "admin2",
                            PasswordHash = "AQAAAAEAACcQAAAAEEJlmMv9wApfVPM7HIlQn7NdMuvOvtenBNv02RPmJCTADfXhnOiJzC1Jgxmrxm2nyQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = 0,
                            TwoFactorEnabled = false,
                            TypeAccount = 0,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LicenseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<int>("MaxSize")
                        .HasColumnType("int");

                    b.Property<decimal>("MaxSpeed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("MonitorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId")
                        .IsUnique()
                        .HasFilter("[DriverId] IS NOT NULL");

                    b.HasIndex("MonitorId")
                        .IsUnique()
                        .HasFilter("[MonitorId] IS NOT NULL");

                    b.HasIndex("RouteId")
                        .IsUnique()
                        .HasFilter("[RouteId] IS NOT NULL");

                    b.ToTable("Buses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DriverId = 1,
                            IsDeleted = false,
                            LicenseCode = "29S6-81655",
                            MaxSize = 12,
                            MaxSpeed = 45m,
                            MonitorId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            Name = "Xe 01",
                            RouteId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            DriverId = 2,
                            IsDeleted = false,
                            LicenseCode = "29S6-81656",
                            MaxSize = 10,
                            MaxSpeed = 40m,
                            MonitorId = new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                            Name = "Xe 02",
                            RouteId = 2,
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            DriverId = 3,
                            IsDeleted = false,
                            LicenseCode = "29S6-81657",
                            MaxSize = 22,
                            MaxSpeed = 35m,
                            MonitorId = new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                            Name = "Xe 03",
                            RouteId = 3,
                            Status = 1
                        });
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Định công",
                            Dob = new DateTime(2020, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "abc@gmail.com",
                            IsDeleted = false,
                            Name = "Nguyễn Sơn",
                            PhoneNumber = "0364735051",
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "Định công",
                            Dob = new DateTime(1998, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "abc@gmail.com",
                            IsDeleted = false,
                            Name = "Nguyễn Hoàng",
                            PhoneNumber = "0364735052",
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            Address = "Định công",
                            Dob = new DateTime(1998, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "abc@gmail.com",
                            IsDeleted = false,
                            Name = "Nguyễn Minh Đức",
                            PhoneNumber = "0364735053",
                            Status = 1
                        });
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Notify", b =>
                {
                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("TimeSent")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.ToTable("Notifies");
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Point", b =>
                {
                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desctiption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Distance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RouteCode")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeDropOff")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimePickUp")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Routes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Distance = 12.34m,
                            IsDeleted = false,
                            Name = "Tuyến 01",
                            RouteCode = "R001",
                            Status = 1,
                            TimeDropOff = new TimeSpan(0, 17, 0, 0, 0),
                            TimePickUp = new TimeSpan(0, 7, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            Distance = 10.34m,
                            IsDeleted = false,
                            Name = "Tuyến 02",
                            RouteCode = "R002",
                            Status = 1,
                            TimeDropOff = new TimeSpan(0, 17, 0, 0, 0),
                            TimePickUp = new TimeSpan(0, 7, 0, 0, 0)
                        },
                        new
                        {
                            Id = 3,
                            Distance = 77.15m,
                            IsDeleted = false,
                            Name = "Tuyến 03",
                            RouteCode = "R003",
                            Status = 1,
                            TimeDropOff = new TimeSpan(0, 17, 0, 0, 0),
                            TimePickUp = new TimeSpan(0, 7, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("BusTracking.Data.Entities.RouteStop", b =>
                {
                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("StopId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("RouteId", "StopId");

                    b.HasIndex("StopId");

                    b.ToTable("RouteStops");
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(10,8)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(11,8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NumberOfStudents")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusId");

                    b.HasIndex("ParentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Bus", b =>
                {
                    b.HasOne("BusTracking.Data.Entities.Driver", "Driver")
                        .WithOne("Bus")
                        .HasForeignKey("BusTracking.Data.Entities.Bus", "DriverId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BusTracking.Data.Entities.AppUser", "Monitor")
                        .WithOne("Bus")
                        .HasForeignKey("BusTracking.Data.Entities.Bus", "MonitorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BusTracking.Data.Entities.Route", "Route")
                        .WithOne("Bus")
                        .HasForeignKey("BusTracking.Data.Entities.Bus", "RouteId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("BusTracking.Data.Entities.RouteStop", b =>
                {
                    b.HasOne("BusTracking.Data.Entities.Route", "Route")
                        .WithMany("RouteStops")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusTracking.Data.Entities.Stop", "Stop")
                        .WithMany("RouteStops")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusTracking.Data.Entities.Student", b =>
                {
                    b.HasOne("BusTracking.Data.Entities.Bus", "Bus")
                        .WithMany("Students")
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusTracking.Data.Entities.AppUser", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
