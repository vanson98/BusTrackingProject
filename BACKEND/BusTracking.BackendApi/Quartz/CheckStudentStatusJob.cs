using BusTracking.Application.Catalog.StudentService;
using BusTracking.BackendApi.SignalRHub;
using BusTracking.Data.EF;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Students;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.Quartz
{
    public class CheckStudentStatusJob : IJob
    {
        public IConfiguration _appConfiguration { get; }
        private readonly IHubContext<BusTrackingHub> _hubContext;
        public CheckStudentStatusJob(IConfiguration configuration, IHubContext<BusTrackingHub> hubContext)
        {
            // Khởi tạo appConfig
            this._appConfiguration = configuration;
            // Khởi tạo Hub
            _hubContext = hubContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            
            var dbContext = this.InitDbContext();
            using (dbContext)
            {
                Console.WriteLine("Checking");
                IStudentService _studentService = new StudentService(dbContext);
                List<WarningCheckInDto> listWarning = await _studentService.CheckStudentStatus();
                foreach (var item in listWarning)
                {
                    await _hubContext.Clients.User(item.TeacherId).SendAsync("ReceiveNotication", item);
                    await _hubContext.Clients.User(item.MonitorId).SendAsync("ReceiveNotication", item);
                    await _hubContext.Clients.User(item.ParentId).SendAsync("ReceiveNotication", item);
                }
            }
            
        }

        public BusTrackingDbContext InitDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusTrackingDbContext>();
            optionsBuilder.UseSqlServer(this._appConfiguration.GetConnectionString(SystemConstants.MainConnectionString));
            return new BusTrackingDbContext(optionsBuilder.Options);
        }
    }
}
