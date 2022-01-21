using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkOnlineChecking.Services.QuartzJob
{
    public class DataJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DataJob(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var statisticUpdate = scope.ServiceProvider.GetService<IStatisticUpdate>();

                await statisticUpdate.PostStatisticAsync();
            }
        }
    }
}
