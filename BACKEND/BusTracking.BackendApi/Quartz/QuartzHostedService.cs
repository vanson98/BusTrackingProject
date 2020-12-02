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
        private JobMetadata _jobMetadataReset;
        private JobMetadata _jobMetadataCheck;
        public IScheduler SchedulerResetStudentStatus { get; set; }
        public IScheduler SchedulerCheckStudentStatus { get; set; }
        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory)
        {
            this._jobFactory = jobFactory;
            this._schedulerFactory = schedulerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Lịch reset trạng thái học sinh
            SchedulerResetStudentStatus = await _schedulerFactory.GetScheduler();
            SchedulerResetStudentStatus.JobFactory = _jobFactory;
            this._jobMetadataReset = new JobMetadata(Guid.NewGuid(), typeof(UpdateStudentStatusJob), "Reset Student Status Job", "0 0 0 * * ?");
            var jobReset = CreateJob(_jobMetadataReset);
            var triggerReset = CreateTrigger(_jobMetadataReset);
            await SchedulerResetStudentStatus.ScheduleJob(jobReset, triggerReset, cancellationToken);
            await SchedulerResetStudentStatus.Start(cancellationToken);
            //Lập lịch check student status
            SchedulerCheckStudentStatus = await _schedulerFactory.GetScheduler();
            SchedulerCheckStudentStatus.JobFactory = _jobFactory;
            this._jobMetadataCheck = new JobMetadata(Guid.NewGuid(), typeof(CheckStudentStatusJob), "Check Student Status Job", "0 */1 * * * ?");
            var jobCheck = CreateJob(_jobMetadataCheck);
            var triggerCheck = CreateTrigger(_jobMetadataCheck);
            await SchedulerCheckStudentStatus.ScheduleJob(jobCheck, triggerCheck, cancellationToken);
            await SchedulerCheckStudentStatus.Start(cancellationToken);
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
            await SchedulerResetStudentStatus?.Shutdown(cancellationToken);
        }
    }
}
