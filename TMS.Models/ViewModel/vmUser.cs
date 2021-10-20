using System;
using System.Collections.Generic;
using System.Text;
using TMS.Models.Model;

namespace TMS.Models.ViewModel
{
    public class vmUser
    {
        public User User { get; set; }
        public string JWTKey { get; set; }
    }
}
