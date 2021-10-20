using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    class TimeSheet
    {
        public int ID { get; set; }
        public DateTime DayDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public User rUser { get; set; }
        

    }
}
