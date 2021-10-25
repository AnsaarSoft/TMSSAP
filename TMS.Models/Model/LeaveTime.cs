using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMS.Models.Model
{
    public class LeaveTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public User rUser { get; set; }
        public TimeSheet rTimeSheet { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
    }
}
