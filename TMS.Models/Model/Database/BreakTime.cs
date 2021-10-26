using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TMS.Models.Model
{
    public class BreakTime
    {
        public int ID { get; set; }
        public int rUser { get; set; }
        public int rTimeSheet { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
