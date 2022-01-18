using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VkOnlineChecking.Data;
using VkOnlineChecking.Entities;
using VkOnlineChecking.Entities.VkJson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VkOnlineChecking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesStatisticController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly string _token = "47f3e7dd47f3e7dd47f3e7dd62478904f5447f347f3e7dd261661e3ad0b667a6709e7fa";
        private readonly string _version = "5.131";

        public ProfilesStatisticController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/ProfilesStatisticController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileStatistic>>> GetProfilesStatistic()
        {
            return await _db.ProfileStatistics.ToListAsync();
        }

        // GET api/ProfilesStatisticController/5
        [HttpGet("{profileUri}")]
        public async Task<ActionResult<IEnumerable<ProfileStatistic>>> GetProfileStatistic(string profileUri)
        {
            var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.ProfileUri == profileUri);
            if (profile == null)
            {
                return NotFound();
            }
            var profileStatistic = await _db.ProfileStatistics.Where(p => p.ProfileId == profile.Id).ToListAsync();
            if (profileStatistic == null)
            {
                return new List<ProfileStatistic>();
            }

            return profileStatistic;
        }

        // POST api/<ProfilesStatisticController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProfileStatistic>>> PostStatistic(string profileUri)
        {
            var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.ProfileUri == profileUri);
            if (profile == null)
            {
                return NotFound();
            }
            List<string> users = new List<string>();
            users.Add(profile.ProfileUri);
            List<VkUser> VkUsers = await GetResponseAsync(users);
            foreach (var user in VkUsers)
            {
                _db.ProfileStatistics.Add(new ProfileStatistic {
                    DateTime = DateTime.UtcNow,
                    ProfileStatus = user.online,
                    ProfileId = _db.Profiles.FirstOrDefault(p => p.ProfileUri == user.domain).Id
                });
            }
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetProfileStatistic", new { id = profile.Id }, profile);
        }

        /*
                // DELETE api/<ProfilesStatisticController>/5
                [HttpDelete("{id}")]
                public void Delete(int id)
                {
                }*/

        private async Task<List<VkUser>> GetResponseAsync(IEnumerable<string> users)
        {
            StringBuilder usersSb = new StringBuilder();

            foreach (var user in users)
            {
                usersSb.Append(user + ",");
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
