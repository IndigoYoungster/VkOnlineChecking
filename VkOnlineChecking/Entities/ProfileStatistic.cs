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
        public string DateTime { get; set; }
        public int ProfileStatus { get; set; }

        [Required]
        public virtual Profile Profile { get; set; }
    }
}
