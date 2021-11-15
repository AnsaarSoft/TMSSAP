using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    public class UserApproval
    {
        public int ID { get; set; }
        public string UserCode { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int rDocument { get; set; }
        public int rUser { get; set; }
        
    }
}
