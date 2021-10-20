using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Models.Model
{
    public class ResponseMessage
    {
        public bool isSuccess { get; set; }
        public User? UserInfo { get; set; }
        public string ErrorMessage { get; set; }
        public string JWTKey { get; set; }
    }
}
