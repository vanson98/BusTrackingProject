using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusTracking.Data.EF
{
    public class BusTrackingDbContextFactory : IDesignTimeDbContextFactory<BusTrackingDbContext>
    {
        public BusTrackingDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BusTrackingDb");

            var optionsBuilder = new DbContextOptionsBuilder<BusTrackingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BusTrackingDbContext(optionsBuilder.Options);

        }
    }
}
