using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VkOnlineChecking.Data;
using VkOnlineChecking.Entities;
using VkOnlineChecking.Methods;

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

        // GET: api/Profiles/{profileUri}
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

        // POST: api/Profiles/{profileUri}
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

        // DELETE: api/Profiles/{profileUri}
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
    }
}
