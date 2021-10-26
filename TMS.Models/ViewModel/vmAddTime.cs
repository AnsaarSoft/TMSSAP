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
            oTime = new mTimeSheet();
            flgSuccess = false;
            Message = "";
            oLeave = new mLeaveTime();
            oBreak = new mBreakTime();
        }
        public bool flgSuccess { get; set; }
        public string Message { get; set; }
        public mTimeSheet oTime { get; set; }
        public mLeaveTime oLeave { get; set; }
        public mBreakTime oBreak { get; set; }
    }
}
