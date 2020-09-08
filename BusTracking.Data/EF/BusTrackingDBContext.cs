using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using BusTracking.Data.FluentConfigModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using BusTracking.Data.Extension;

namespace BusTracking.Data.EF
{
    public class BusTrackingDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public BusTrackingDbContext(DbContextOptions<BusTrackingDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply config fluent api for entity
            modelBuilder.ApplyConfiguration(new BusConfiguration());
            modelBuilder.ApplyConfiguration(new DriverConfiguration());
            modelBuilder.ApplyConfiguration(new NotifyConfiguration());
            modelBuilder.ApplyConfiguration(new PointConfiguration());
            modelBuilder.ApplyConfiguration(new RoundConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new RouteRoundConfiguration());
            modelBuilder.ApplyConfiguration(new RouteStopConfiguration());
            modelBuilder.ApplyConfiguration(new StopConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            // Phần Entity xác thực 
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            // Fake Data
            modelBuilder.Seed();
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteRound> RouteRounds { get; set; }
        public DbSet<RouteStop> RouteStops {get;set;}
        public DbSet<Stop> Stops  {get;set;}
        public DbSet<Student> Students  {get;set;}
    }
}
