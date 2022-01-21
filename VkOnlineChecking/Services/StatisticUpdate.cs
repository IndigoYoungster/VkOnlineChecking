using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VkOnlineChecking.Data;
using VkOnlineChecking.Entities;
using VkOnlineChecking.Entities.VkJson;

namespace VkOnlineChecking.Services
{
    public class StatisticUpdate : IStatisticUpdate
    {
        private readonly ApplicationDbContext _db;
        private readonly string _token = "47f3e7dd47f3e7dd47f3e7dd62478904f5447f347f3e7dd261661e3ad0b667a6709e7fa";
        private readonly string _version = "5.131";

        public StatisticUpdate(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task PostStatisticAsync()
        {
            List<Profile> profiles = await _db.Profiles.ToListAsync();
            List<VkUser> VkUsers = await GetResponseAsync(profiles);
            ProfileStatistic profileStatistic;
            string dateTimeNow = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss");
            foreach (var user in VkUsers)
            {
                var profile = _db.Profiles.FirstOrDefault(p => p.ProfileUri == user.domain);

                profileStatistic = new ProfileStatistic
                {
                    //DateTime = DateTime.UtcNow,
                    DateTime = dateTimeNow,
                    ProfileStatus = user.online,
                    //ProfileId = profileId
                    Profile = profile
                };
                _db.ProfileStatistics.Add(profileStatistic);

/*                //Profile profile = _db.Profiles.FirstOrDefault(p => p.Id == profileId);
                profile.ProfileStatistics.Add(profileStatistic);
                _db.Profiles.Update(profile);*/
            }
            await _db.SaveChangesAsync();
        }

        public async Task<List<VkUser>> GetResponseAsync(IEnumerable<Profile> profiles)
        {
            StringBuilder usersSb = new StringBuilder();

            foreach (var user in profiles)
            {
                usersSb.Append(user.ProfileUri + ",");
            }
            usersSb.Remove(usersSb.Length - 1, 1);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://api.vk.com/method/users.get?user_ids={usersSb}&access_token={_token}&v={_version}&fields=online,domain");
            var content = await response.Content.ReadAsStringAsync();

            var jsonUsers = JsonConvert.DeserializeObject<VkResponse>(content);

            return jsonUsers.Response;
        }
    }
}
