﻿using System;

namespace TMS.Models.Model
{
    public class User
    {
        public int ID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SBOId { get; set; }
        public double LeaveHours { get; set; }

    }
}