using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VkOnlineChecking.Entities
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfileUri { get; set; }
        [Required]
        public string UserName { get; set; }

        public List<ProfileStatistic> ProfileStatistics { get; set; }
    }
}
