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
    public partial class UserReport
    {

        #region Variable
        bool flgBusy = false;
        DateTime? FromDate = DateTime.Now;
        DateTime? ToDate = DateTime.Now;
        List<User> UserList = new List<User>();
        User oUser = new User();
        List<vmReportSheet> TimesheetList = new List<vmReportSheet>();

        private List<BreadcrumbItem> oBreadList = new List<BreadcrumbItem>
        {
            new BreadcrumbItem ("Home", href:"/"),
            new BreadcrumbItem ("User Reports", href:"//timesheet/userreport")
        };
        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        IAccountServices oServiceAccount { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        ISnackbar toast { get; set; }

        #endregion

        #region Function

        private void SuccessMessage(string message)
        {
            toast.Add(message, Severity.Success);
        }

        private void ErrorMessage(string message)
        {
            toast.Add(message, Severity.Error);
        }

        protected async override Task OnInitializedAsync()
        {
            UserList = await oServiceAccount.GetAllUser();
            vmReportSheet vmReportSheet = new vmReportSheet();
            TimesheetList.Add(vmReportSheet);
            oUser = UserList.FirstOrDefault();
        }

        private async Task ShowReport()
        {
            try
            {
                string fromdate = FromDate.Value.ToString("dd/MM/yyyy");
                string todate = ToDate.Value.ToString("dd/MM/yyyy");
                TimesheetList = await oService.GetUserReport(fromdate, todate, oUser.ID, oUser.UserName);
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
        }

        private void ShowLeaveDetails(int prmID)
        {
            try
            {
                var SelectedSheet = TimesheetList.Where(a => a.ID == prmID).FirstOrDefault();
                SelectedSheet.flgShowLeave = !SelectedSheet.flgShowLeave;
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
        }

        private void ShowBreakDetails(int prmID)
        {
            try
            {
                var SelectedSheet = TimesheetList.Where(a => a.ID == prmID).FirstOrDefault();
                SelectedSheet.flgShowBreak = !SelectedSheet.flgShowBreak;
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
        }

        #endregion
    }
}
