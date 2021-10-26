using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    public class mLeaveTime
    {
        public int ID { get; set; }
        public int rUser { get; set; }
        public int rTimeSheet { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public LeaveTime Mapping()
        {
            LeaveTime oNew = new LeaveTime();
            try
            {
                oNew.ID = this.ID;
                oNew.rTimeSheet = this.rTimeSheet;
                oNew.rUser = this.rUser;
                if (!string.IsNullOrEmpty(this.StartTime))
                    oNew.StartTime = Convert.ToDateTime(this.StartTime);
                if (!string.IsNullOrEmpty(this.EndTime))
                    oNew.EndTime = Convert.ToDateTime(this.EndTime);
            }
            catch (Exception ex)
            {
            }
            return oNew;
        }

    }
}
