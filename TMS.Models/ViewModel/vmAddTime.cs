using System;
using System.Collections.Generic;
using System.Text;
using TMS.Models.Model;

namespace TMS.Models.ViewModel
{
    public class vmAddTime
    {
        public vmAddTime()
        {
            oTime = new TimeSheet();
            flgSuccess = false;
            Message = "";
            oLeave = new LeaveTime();
            oBreak = new BreakTime();
        }
        public bool flgSuccess { get; set; }
        public string Message { get; set; }
        public TimeSheet oTime { get; set; }
        public LeaveTime oLeave { get; set; }
        public BreakTime oBreak { get; set; }
    }
}
