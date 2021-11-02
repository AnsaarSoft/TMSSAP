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
        
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var oUser = await oUserRepository.ValidateLogin(user);
                if (oUser != null)
                {
                    ResponseMessage oResponse = new ResponseMessage();
                    oResponse.isSuccess = true;
                    oResponse.JWTKey = "128jSHu3920kdsk483cnm8472";
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
        
        [HttpPost, Route("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                var oUser = await oUserRepository.AddUser(user);
                if (oUser != null)
                {
                    return Ok(oUser);
                }
                else
                {
                    return BadRequest("Invalid user or password.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid user or password.");
            }
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var oUser = await oUserRepository.UpdateUser(user);
                if (oUser != null)
                {
                    return Ok(oUser);
                }
                else
                {
                    return BadRequest("Invalid user or password.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid user or password.");
            }
        }

        [HttpGet, Route("getall")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var oCollection = await oUserRepository.GetAllUsers();
                if (oCollection != null)
                {
                    return Ok(oCollection);
                }
                else
                {
                    return BadRequest("Invalid user or password.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid user or password.");
            }
        }

    }
}
