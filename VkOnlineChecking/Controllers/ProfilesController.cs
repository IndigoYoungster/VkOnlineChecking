using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VkOnlineChecking.Data;
using VkOnlineChecking.Entities;

namespace VkOnlineChecking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private CreateResponseString responseString;

        public ProfilesController(ApplicationDbContext db)
        {
            _db = db;
            responseString = new CreateResponseString();
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<string>> GetProfiles()
        {
            var profiles = await _db.Profiles.ToListAsync();
            return responseString.CreateString(profiles);
        }

        // GET: api/Profiles/indigo
        [HttpGet("{profileUri}")]
        public async Task<ActionResult<string>> GetProfile(string profileUri)
        {
            var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.ProfileUri == profileUri);

            if (profile == null)
            {
                return "Profile not found";
            }

            return responseString.CreateString(profile);
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{profileUri}")]
        public async Task<IActionResult> PostProfile(string profileUri)
        {
            Profile profile = new Profile
            {
                ProfileUri = profileUri,
            };
            _db.Profiles.Add(profile);
            await _db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Profiles/indigo_youngster
        [HttpDelete("{profileUri}")]
        public async Task<IActionResult> DeleteProfile(string profileUri)
        {
            var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.ProfileUri == profileUri);
            if (profile == null)
            {
                return NotFound();
            }

            _db.Profiles.Remove(profile);
            await _db.SaveChangesAsync();

            return Ok();
        }

        private bool ProfileExists(int id)
        {
            return _db.Profiles.Any(e => e.Id == id);
        }
    }
}
