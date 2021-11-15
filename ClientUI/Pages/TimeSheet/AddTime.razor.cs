using ClientUI.Helpers;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
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

        DateTime? DayDate = DateTime.UtcNow;
        TimeSpan? StartTime = new TimeSpan(8,0,0);
        TimeSpan? EndTime = new TimeSpan(16,0,0);
        TimeSpan? LeaveStartTime = new TimeSpan(8, 0, 0);
        TimeSpan? LeaveEndTime = new TimeSpan(16, 0, 0);
        TimeSpan? BreakStartTime = new TimeSpan(13, 0, 0);
        TimeSpan? BreakEndTime = new TimeSpan(13, 30, 0);
        bool flgLeave = false;
        bool flgBreak = false;
        bool ShowBreak = false;

        vmAddTime oModel = new vmAddTime();

        private List<BreadcrumbItem> oBreadList = new List<BreadcrumbItem>
        {
            new BreadcrumbItem ("Home", href:"/"),
            new BreadcrumbItem ("New Time Sheets", href:"/timesheet/addtime")
        };

        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        ISnackbar toast { get; set; }

        #endregion

        #region Function

        protected override async Task<bool> OnInitializedAsync()
        {
            await oService.Initiallize();
            if (oService.oUser is not null)
            {
                ShowBreak = oService.oUser.User.flgBreak;
            }
            //return base.OnInitializedAsync();
            return true;
        }

        public async Task AddTimeRecord()
        {
            flgBusy = true;
            try
            {
                oModel.oTime.DayDate = DayDate.GetValueOrDefault().ToString();
                oModel.oTime.StartTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, StartTime.Value.Hours, StartTime.Value.Minutes, 0).ToString();
                oModel.oTime.EndTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, EndTime.Value.Hours, EndTime.Value.Minutes, 0).ToString();
                if(flgLeave)
                {
                    oModel.oTime.flgLeave = flgLeave;
                    oModel.oLeave = new mLeaveTime();
                    oModel.oLeave.StartTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, LeaveStartTime.Value.Hours, LeaveStartTime.Value.Minutes, 0).ToString();
                    oModel.oLeave.EndTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, LeaveEndTime.Value.Hours, LeaveEndTime.Value.Minutes, 0).ToString();
                }
                if(flgBreak)
                {
                    oModel.oTime.flgBreak = flgBreak;
                    oModel.oBreak = new mBreakTime();
                    oModel.oBreak.StartTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, BreakStartTime.Value.Hours, BreakStartTime.Value.Minutes, 0).ToString();
                    oModel.oBreak.EndTime = new DateTime(DayDate.Value.Year, DayDate.Value.Month, DayDate.Value.Day, BreakEndTime.Value.Hours, BreakEndTime.Value.Minutes, 0).ToString();
                }

                var CheckTime = await oService.AddTimeSheet(oModel);
                if(CheckTime == null)
                {
                    //"Error"
                    ErrorMessage("Record didn't add, something went wrong.");
                }
                else
                {
                    //Success
                    SuccessMessage("Record added successfully.");
                    ClearRecord();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage("Something went wrong.");
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        public async Task CancelTime()
        {
            oNavigation.NavigateTo("/timesheet/dailytimesheet");
        }

        public void ClearRecord()
        {
            DayDate = DateTime.Now;
            StartTime = new TimeSpan(8, 0, 0);
            EndTime = new TimeSpan(16, 0, 0);
            LeaveStartTime = new TimeSpan(8, 0, 0);
            LeaveEndTime = new TimeSpan(16, 0, 0);
            BreakStartTime = new TimeSpan(13, 0, 0);
            BreakEndTime = new TimeSpan(13, 30, 0);
            flgLeave = false;
            flgBreak = false;
        }

        public void SuccessMessage(string message)
        {
            toast.Add(message, Severity.Success);
        }

        public void ErrorMessage(string message)
        {
            toast.Add(message, Severity.Error);
        }

        #endregion
    }
}
