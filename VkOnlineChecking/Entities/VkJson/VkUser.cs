﻿using System;

namespace VkOnlineChecking.Entities.VkJson
{
    public class VkUser
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool can_access_closed { get; set; }
        public bool is_closed { get; set; }
        public int online { get; set; }
        public string domain { get; set; }
    }
}
