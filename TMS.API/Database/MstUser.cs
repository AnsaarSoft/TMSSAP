using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.API.Database
{
    public class MstUser
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
