using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    class BreakTime
    {
        public int ID { get; set; }
        public User rUser { get; set; }
        public TimeSheet rTimeSheet { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool flgFullDay { get; set; }

    }
}
