using System;
using System.ComponentModel.DataAnnotations;

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
