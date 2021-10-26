using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TMS.Models.Model
{
    public class TimeSheet
    {
        public int ID { get; set; }
        public DateTime DayDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public int rUser { get; set; }
        public bool flgLeave { get; set; }
        public bool flgBreak { get; set; }
    }
}
