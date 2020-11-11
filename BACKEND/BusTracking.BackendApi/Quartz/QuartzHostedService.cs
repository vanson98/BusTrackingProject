using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.Quartz
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly JobMetadata _jobMetadata;
        public IScheduler Scheduler { get; set; }
        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            JobMetadata jobMetadata)
        {
            this._jobFactory = jobFactory;
            this._schedulerFactory = schedulerFactory;
            this._jobMetadata = jobMetadata;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = _jobFactory;
            var job = CreateJob(_jobMetadata);
            var trigger = CreateTrigger(_jobMetadata);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder
            .Create(jobMetadata.JobType)
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder
            .Create()
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithCronSchedule(jobMetadata.CronExpression)
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
