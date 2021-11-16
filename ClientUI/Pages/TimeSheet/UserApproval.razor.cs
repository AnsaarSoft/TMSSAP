using ClientUI.Helpers;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.ViewModel;

namespace ClientUI.Pages.TimeSheet
{
    public partial class UserApproval
    {
        #region Variable

        List<vmApprovals> oCollection = new();
        HashSet<vmApprovals> SelectedApproval = new();
        bool flgBusy = false;
        private List<BreadcrumbItem> oBreadList = new List<BreadcrumbItem>
        {
            new BreadcrumbItem ("Home", href:"/"),
            new BreadcrumbItem ("User Approval", href:"/timesheet/userapproval")
        };
        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        ISnackbar toast { get; set; }
        #endregion

        #region Function

        protected async override Task OnInitializedAsync()
        {
            await GetData();
            //return base.OnInitializedAsync();
        }

        private async Task ApproveRecords(int id)
        {
            flgBusy = true;
            try
            {
                //oCollection.Remove();
                var item = oCollection.Where(a => a.ID == id).FirstOrDefault();
                if (item is not null)
                {
                    var result = await oService.ApproveTimesheet(item);
                    if(result)
                    {
                        oCollection.Remove(item);
                        SuccessMessage($"Document number {id} approved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        private async Task RejectRecords(int id)
        {
            flgBusy = true;
            try
            {
                var item = oCollection.Where(a => a.ID == id).FirstOrDefault();
                if (item is not null)
                {
                    var result = await oService.RejectTimesheet(item);
                    if(result)
                    {
                        SuccessMessage($"Document number {id} rejected successfully.");
                        oCollection.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        private async Task GetData()
        {
            try
            {
                oCollection = await oService.GetAllPendingDocument();
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                ErrorMessage("Something went wrong.");
            }
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
