using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProfilesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _db.Profiles.ToListAsync();
        }

        // GET: api/Profiles/Nikita
        [HttpGet("{profileUri}")]
        public async Task<ActionResult<Profile>> GetProfile(string profileUri)
        {
            var profile = await _db.Profiles.FirstOrDefaultAsync(p => p.ProfileUri == profileUri);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            _db.Profiles.Add(profile);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
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

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return _db.Profiles.Any(e => e.Id == id);
        }

        /*        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _db.Entry(profile).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
*/
    }
}
