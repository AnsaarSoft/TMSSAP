using System;
using System.Collections.Generic;
using System.Text;
using TMS.Models.Model;

namespace TMS.Models.ViewModel
{
    public class vmTimeSheet
    {
        public bool flgSuccess { get; set; }
        public DateTime dtFrom { get; set; }
        public DateTime dtTo { get; set; }
        public User oUser { get; set; }
        public List<TimeSheet> oCollection { get; set; }

    }
}
