using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VkOnlineChecking.Entities
{
    public class ProfileStatistic
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string ProfileStatus { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
