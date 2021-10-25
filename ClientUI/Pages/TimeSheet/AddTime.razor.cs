using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace ClientUI.Pages.TimeSheet
{
    public partial class AddTime
    {

        #region Variable
        bool flgBusy = false;

        DateTime? DayDate = DateTime.Now;
        TimeSpan? StartTime = new TimeSpan(8,0,0);
        TimeSpan? EndTime = new TimeSpan(16,0,0);
        TimeSpan? LeaveStartTime = new TimeSpan(8, 0, 0);
        TimeSpan? LeaveEndTime = new TimeSpan(16, 0, 0);
        TimeSpan? BreakStartTime = new TimeSpan(8, 0, 0);
        TimeSpan? BreakEndTime = new TimeSpan(16, 0, 0);
        bool flgLeave = false;
        bool flgBreak = false;

        vmAddTime oModel = new vmAddTime();

        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }


        #endregion

        #region Function

        protected override async Task OnInitializedAsync()
        {
            await FormInitiallize();
        }

        public async Task FormInitiallize()
        {
            await Task.Run(() => { 

            });
        }

        public async Task AddTimeRecord()
        {
            try
            {
                oModel.oTime.DayDate = DayDate.GetValueOrDefault();
                oModel.oTime.StartTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, StartTime.Value.Hours, StartTime.Value.Minutes, 0);
                oModel.oTime.EndTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, EndTime.Value.Hours, EndTime.Value.Minutes, 0);
                if(flgLeave)
                {
                    oModel.oTime.flgLeave = flgLeave;
                    oModel.oLeave = new LeaveTime();
                    oModel.oLeave.StartTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, LeaveStartTime.Value.Hours, LeaveStartTime.Value.Minutes, 0);
                    oModel.oLeave.EndTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, LeaveEndTime.Value.Hours, LeaveEndTime.Value.Minutes, 0);
                }
                if(flgBreak)
                {
                    oModel.oTime.flgBreak = flgBreak;
                    oModel.oBreak = new BreakTime();
                    oModel.oBreak.StartTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, BreakStartTime.Value.Hours, BreakStartTime.Value.Minutes, 0);
                    oModel.oBreak.EndTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, BreakEndTime.Value.Hours, BreakEndTime.Value.Minutes, 0);
                }

                var CheckTime = await oService.AddTimeSheet(oModel);
                if(CheckTime == null)
                {
                    //"Error"
                }
                else
                {
                    //Success
                    ClearRecord();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task CancelTime()
        {
            oNavigation.NavigateTo("/timesheet/dailytimesheet");
        }

        public async Task ClearRecord()
        {
            DayDate = DateTime.Now;
            StartTime = new TimeSpan(8, 0, 0);
            EndTime = new TimeSpan(16, 0, 0);
            LeaveStartTime = new TimeSpan(8, 0, 0);
            LeaveEndTime = new TimeSpan(16, 0, 0);
            BreakStartTime = new TimeSpan(8, 0, 0);
            BreakEndTime = new TimeSpan(16, 0, 0);
            flgLeave = false;
            flgBreak = false;
        }

        #endregion
    }
}
