using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    public class mUser
    {
        public string ID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SBOId { get; set; }
        public string LeaveHours { get; set; }
        public string flgSuper { get; set; }

        public User Mapping(mUser ouser)
        {
            User oNew = new User();
            try
            {
                if(!string.IsNullOrEmpty(ouser.ID))
                    oNew.ID = Convert.ToInt32(ouser.ID);
                if(!string.IsNullOrEmpty(ouser.UserCode))
                    oNew.UserCode = Convert.ToString(ouser.UserCode);
                if(!string.IsNullOrEmpty(ouser.UserName))
                    oNew.UserName = Convert.ToString(ouser.UserName);
                if(!string.IsNullOrEmpty(ouser.Password))
                    oNew.Password = Convert.ToString(ouser.Password);
                if(!string.IsNullOrEmpty(ouser.Email))
                    oNew.Email = Convert.ToString(ouser.Email);
                if(!string.IsNullOrEmpty(ouser.SBOId))
                    oNew.SBOId = Convert.ToString(ouser.SBOId);
                if (!string.IsNullOrEmpty(ouser.LeaveHours))
                    oNew.LeaveHours = Convert.ToInt32(ouser.LeaveHours);
                if (!string.IsNullOrEmpty(ouser.flgSuper))
                    oNew.flgSuper = Convert.ToBoolean(ouser.flgSuper);

            }
            catch (Exception ex)
            {
                oNew = null;
            }
            return oNew;
        }
    }
}
