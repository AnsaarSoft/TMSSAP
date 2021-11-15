using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.ViewModel
{
    public class vmApprovals
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string LeaveDate { get; set; }
        public string LeaveStart { get; set; }
        public string LeaveEnd { get; set; }
        public double TotalHour { get; set; }
        public string Status { get; set; }
    }
}
