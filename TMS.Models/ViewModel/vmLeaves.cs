using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.ViewModel
{
    public class vmLeaves
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double TotalHour { get; set; }
    }

    public class vmBreaks
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double TotalHour { get; set; }
    }
}
