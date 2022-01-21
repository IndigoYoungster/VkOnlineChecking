using System;
using System.Threading.Tasks;

namespace VkOnlineChecking.Services
{
    public interface IStatisticUpdate
    {
        Task PostStatisticAsync();
    }
}
