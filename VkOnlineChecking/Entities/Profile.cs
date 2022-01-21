using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VkOnlineChecking.Entities
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfileUri { get; set; }

        public virtual ICollection<ProfileStatistic> ProfileStatistics { get; set; }
    }
}
