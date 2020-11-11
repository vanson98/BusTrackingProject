using BusTracking.Application.Catalog.StudentService;
using BusTracking.Data.EF;
using BusTracking.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.Quartz
{
    public class UpdateStudentStatusJob : IJob
    {
       
        public BusTrackingDbContext _dbContext;
        public StudentService _studentService;
        public IConfiguration _appConfiguration { get; }
        public UpdateStudentStatusJob(IConfiguration configuration)
        {
            this._appConfiguration = configuration;
            var optionsBuilder = new DbContextOptionsBuilder<BusTrackingDbContext>();
            optionsBuilder.UseSqlServer(_appConfiguration.GetConnectionString(SystemConstants.MainConnectionString));
            this._dbContext = new BusTrackingDbContext(optionsBuilder.Options);
            this._studentService = new StudentService(this._dbContext);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var totalCount = await this._studentService.ResetStatus(0);
            Console.WriteLine($"Đã update {totalCount} học sinh");
        }
    }
}
