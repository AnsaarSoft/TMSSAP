using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientUI.Pages.TimeSheet
{
    public partial class UserReport
    {

        #region Variable
        bool flgBusy = false;
        

        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        ISnackbar toast { get; set; }

        #endregion

        #region Function

        public void SuccessMessage(string message)
        {
            toast.Add(message, Severity.Success);
        }

        public void ErrorMessage(string message)
        {
            toast.Add(message, Severity.Error);
        }

        async Task ShowReport()
        {

        }

        #endregion
    }
}
