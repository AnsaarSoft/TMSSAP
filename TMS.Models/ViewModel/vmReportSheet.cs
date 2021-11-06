using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.ViewModel
{
    public class vmReportSheet
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string DayDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double TotalHour { get; set; }
        public bool flgShowLeave { get; set; }
        public bool flgShowBreak { get; set; }

        public List<vmLeaves> LeavesList { get; set; }
        public List<vmBreaks> BreaksList { get; set; }

    }
}
