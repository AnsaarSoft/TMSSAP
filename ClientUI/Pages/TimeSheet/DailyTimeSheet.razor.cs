﻿using ClientUI.Helpers;
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
    public partial class DailyTimeSheet
    {
        #region Variable

        DateTime? FromDate = DateTime.Now;
        DateTime? ToDate = DateTime.Now;
        bool flgBusy = false;

        vmTimeSheet oModel = new vmTimeSheet();

        private List<BreadcrumbItem> oBreadList = new List<BreadcrumbItem>
        {
            new BreadcrumbItem ("Home", href:"/"),
            new BreadcrumbItem ("Time Sheets", href:"/timesheet/dailytimesheet")
        };

        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        ISnackbar toast { get; set; }



        #endregion

        #region Function

        protected override void OnInitialized()
        {
            base.OnInitialized();
            WeekStart();
        }

        public async Task GetData()
        {
            flgBusy = true;
            try
            {
                oModel.dtFrom = FromDate.GetValueOrDefault();
                oModel.dtTo = ToDate.GetValueOrDefault();
                oModel.oCollection = await oService.GetUserTimeSheet(oModel);
                if(oModel.oCollection != null)
                {
                    SuccessMessage("Data loaded successfuly.");
                }
                else
                {
                    ErrorMessage("Data didn't loaded");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage("something went wrong.");
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        public void WeekStart()
        {
            try
            {
                DateTime start = DateTime.Now;  
                DateTime end = DateTime.Now;
                while(start.DayOfWeek != DayOfWeek.Sunday)
                {
                    start = start.AddDays(-1);
                }
                
                while (end.DayOfWeek != DayOfWeek.Saturday)
                {
                    end = end.AddDays(1);
                }
                FromDate = start;
                ToDate = end;
            }
            catch (Exception ex)
            {
                ErrorMessage("something went worng.");
                Logs.Logger(ex);
            }
        }

        public async Task SubmitSheets()
        {
            flgBusy = true;
            try
            {
                await oService.SubmitTimeSheet(oModel);
                await GetData();
            }
            catch (Exception ex)
            {
                ErrorMessage("Something went wrong.");
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        public async Task CancelSheets()
        {
            flgBusy = true;
            try
            {
                await oService.CancelTimeSheet(oModel);
                await GetData();
            }
            catch (Exception ex)
            {
                ErrorMessage("Something went wrong.");
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        public async Task GotoTimeSheetAdd()
        {
            oNavigation.NavigateTo("/timesheet/addtime");
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
