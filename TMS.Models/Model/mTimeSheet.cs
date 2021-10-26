using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    public class mTimeSheet
    {
        public int ID { get; set; }
        public string DayDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }
        public int rUser { get; set; }
        public bool flgLeave { get; set; }
        public bool flgBreak { get; set; }

        public TimeSheet Mapping()
        {
            TimeSheet oNew = new TimeSheet();
            try
            {
                oNew.ID = this.ID;
                if (!string.IsNullOrEmpty(this.DayDate))
                    oNew.DayDate = Convert.ToDateTime(this.DayDate);
                if (!string.IsNullOrEmpty(this.StartTime))
                    oNew.StartTime = Convert.ToDateTime(this.StartTime);
                if (!string.IsNullOrEmpty(this.EndTime))
                    oNew.EndTime = Convert.ToDateTime(this.EndTime);
                oNew.Status = this.Status;
                oNew.rUser = this.rUser;
                oNew.flgBreak = this.flgBreak;
                oNew.flgLeave = this.flgLeave;

            }
            catch (Exception ex)
            {
                oNew = null;
            }
            return oNew;
        }
    }
}
