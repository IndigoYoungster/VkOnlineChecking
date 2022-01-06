using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkOnlineChecking.Entities;

namespace VkOnlineChecking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileStatistic> ProfileStatistics { get; set; }
    }
}
