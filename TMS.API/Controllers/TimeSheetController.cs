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
    public class TimeSheetController : ControllerBase
    {

        private ITimeSheetRepository oTimeSheetRepository;

        public TimeSheetController(ITimeSheetRepository pRepository)
        {
            oTimeSheetRepository = pRepository;
        }

        [Route("getdatabyuser")]
        [HttpPost]
        public async Task<IActionResult> GetUserTimeSheet([FromBody] vmTimeSheet oSheet)
        {
            try
            {
                var ListValue = await oTimeSheetRepository.GetUserData(oSheet);
                if (ListValue != null)
                {
                    return Ok(ListValue);
                }
                else
                {
                    return BadRequest("No record found on specified range.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Some went wrong.");
            }
        }

        [Route("addtime")]
        [HttpPost]
        public async Task<IActionResult> AddTimeSheet([FromBody] vmAddTime oSheet)
        {
            try
            {
                var flgSuccessTime = await oTimeSheetRepository.AddTimeSheet(oSheet);
                if (flgSuccessTime != null)
                {
                    if(oSheet.oTime.flgLeave)
                    {
                        oSheet.oLeave.rTimeSheet = flgSuccessTime.ID;
                        oSheet.oLeave.rUser = flgSuccessTime.rUser;
                        var flgSuccessLeave = await oTimeSheetRepository.AddLeave(oSheet);
                    }
                    if(oSheet.oTime.flgBreak)
                    {
                        oSheet.oBreak.rTimeSheet = flgSuccessTime.ID;
                        oSheet.oBreak.rUser = flgSuccessTime.ID;
                        var flgSuccessBreak = await oTimeSheetRepository.AddBreak(oSheet);
                    }
                    return Ok(oSheet);
                }
                else
                {
                    return BadRequest("Timesheet didn't added successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Some went wrong.");
            }
        }

        
        [HttpPost, Route("submitsheet")]
        public async Task<IActionResult> SubmitTimeSheet([FromBody] vmTimeSheet oSheet)
        {
            try
            {
                await oTimeSheetRepository.SubmitTimeSheet(oSheet);
                oSheet.flgSuccess = true;
                return Ok(oSheet);
            }
            catch (Exception ex)
            {
                return BadRequest("Some went wrong.");
            }
        }

        [HttpPost, Route("cancelsheet")]
        public async Task<IActionResult> CancelTimeSheet([FromBody] vmTimeSheet oSheet)
        {
            try
            {
                await oTimeSheetRepository.CancelTimeSheet(oSheet);
                oSheet.flgSuccess = true;
                return Ok(oSheet);
            }
            catch (Exception ex)
            {
                return BadRequest("Some went wrong.");
            }
        }

    }
}
