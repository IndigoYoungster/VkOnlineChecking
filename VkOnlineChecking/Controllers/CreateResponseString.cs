using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkOnlineChecking.Entities;

namespace VkOnlineChecking.Controllers
{
    public class CreateResponseString
    {
        private StringBuilder stringBuilder;

        private string[] Status;

        public CreateResponseString()
        {
            stringBuilder = new StringBuilder();
            Status = new string[2] { "Offline", "Online" };
        }
        public string CreateString(List<Profile> profiles)
        {
            foreach (var item in profiles)
            {
                stringBuilder.Append($"Id: {item.Id}\n" +
                          $"Profile URI: {item.ProfileUri}\n\n");
            }
            return stringBuilder.ToString();
        }

        public string CreateString(Profile profile)
        {
            if (profile.ProfileStatistics == null)
            {
                return $"Id: {profile.Id}\n" +
                   $"Profile URI: {profile.ProfileUri}";
            }
            else
            {
                stringBuilder.Append($"Statistic for {profile.ProfileUri}\n\n");
                foreach (var item in profile.ProfileStatistics)
                {
                    stringBuilder.Append($"DateTime: {item.DateTime}\n" +
                              $"Online status: {Status[item.ProfileStatus]}\n\n");
                }

                return stringBuilder.ToString();
            }
        }

        public string CreateString(List<ProfileStatistic> profileStatistics)
        {
            foreach (var item in profileStatistics)
            {
                stringBuilder.Append($"Profile URI: {item.Profile.ProfileUri}\n" +
                          $"DateTime: {item.DateTime}\n" +
                          $"Online status: {Status[item.ProfileStatus]}\n\n");
            }
            return stringBuilder.ToString();
        }
    }
}
