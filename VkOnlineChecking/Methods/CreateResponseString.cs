using System;
using System.Collections.Generic;
using System.Text;
using VkOnlineChecking.Entities;

namespace VkOnlineChecking.Methods
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

            return stringBuilder.ToString().Trim();
        }

        public string CreateString(Profile profile)
        {
            return $"Id: {profile.Id}\n" +
                   $"Profile URI: {profile.ProfileUri}";
        }

        public string CreateString(List<ProfileStatistic> profileStatistics)
        {
            foreach (var item in profileStatistics)
            {
                stringBuilder.Append($"Profile URI: {item.Profile.ProfileUri}\n" +
                          $"DateTime: {item.DateTime}\n" +
                          $"Online status: {Status[item.ProfileStatus]}\n\n");
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
