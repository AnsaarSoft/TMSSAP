using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid user and password.");
            }
        }


    }
}
