using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkOnlineChecking.Entities;
using VkOnlineChecking.Entities.VkJson;

namespace VkOnlineChecking.Services
{
    public interface IStatisticUpdate
    {
        Task PostStatisticAsync();
    }
}
