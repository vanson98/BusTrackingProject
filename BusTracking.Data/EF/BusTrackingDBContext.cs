using BusTracking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Data.EF
{
    class BusTrackingDBContext : DbContext
    {
        public BusTrackingDBContext(DbContextOptions<BusTrackingDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
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
