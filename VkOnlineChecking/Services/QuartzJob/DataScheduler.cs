using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;

namespace VkOnlineChecking.Services.QuartzJob
{
    public class DataScheduler
    {
        private static readonly DateTime startTime = DateTime.Parse("12:00:00");
        public static async void Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<DataJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("updateTrigger", "default")
                //.StartAt(startTime) //use this command to start at the right moment
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
