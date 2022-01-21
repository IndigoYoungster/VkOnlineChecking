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
using VkOnlineChecking.Methods;
using VkOnlineChecking.Services;

namespace VkOnlineChecking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesStatisticController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private CreateResponseString responseString;
        private ReportExcel reportExcel;
        private readonly string fileType = "application/xlsx";
        private readonly string fileName = "Report.xlsx";

        public ProfilesStatisticController(ApplicationDbContext db)
        {
            _db = db;
            responseString = new CreateResponseString();
            reportExcel = new ReportExcel();
        }

        // GET: api/ProfilesStatisticController
        [HttpGet]
        public async Task<ActionResult<string>> GetProfilesStatistic()
        {
            var profileStatistics = await _db.ProfileStatistics.Include(p => p.Profile).ToListAsync();
            return responseString.CreateString(profileStatistics);
        }

        // GET api/ProfilesStatisticController/indigo_youngster
        [HttpGet("{profileUri}")]
        public async Task<FileResult> GetProfileStatistic(string profileUri)
        {
            var profile = await _db.Profiles.Include(p => p.ProfileStatistics).FirstOrDefaultAsync(p => p.ProfileUri == profileUri);
            if (profile == null)
            {
                return File(new byte[] { }, fileType, fileName);
            }

            var report = reportExcel.Generate(profile);

            return File(report, fileType, fileName);
            //return responseString.CreateString(profile);
        }


        // DELETE api/<ProfilesStatisticController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profileStatistic = await _db.ProfileStatistics.FirstOrDefaultAsync(i => i.Id == id);
            if (profileStatistic == null)
            {
                return NotFound();
            }

            _db.ProfileStatistics.Remove(profileStatistic);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
