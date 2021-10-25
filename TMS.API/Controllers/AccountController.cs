using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.API.Model;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private IUserRepository oUserRepository;

        public AccountController(IUserRepository userRepository)
        {
            oUserRepository = userRepository;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var oUser = await oUserRepository.ValidateLogin(user);
                if (oUser != null)
                {
                    ResponseMessage oResponse = new ResponseMessage();
                    oResponse.isSuccess = true;
                    oResponse.JWTKey = "muhahahahha";
                    oResponse.UserInfo = oUser;
                    return Ok(oResponse);
                }
                else
                {
                    return BadRequest("Invalid user or password.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid user or password.");
            }
        }


    }
}
