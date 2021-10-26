using Blazored.LocalStorage;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace ClientUI.Pages.Account
{
    public partial class Login
    {
        #region Variables
        
        User oUser = new User();
        bool flgBusy = false;
        string message = string.Empty;

        [Inject]
        IAccountServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        ISnackbar toast { get; set; }
        #endregion

        #region Functions

        public async Task LoginUser()
        {
            flgBusy = true;
            var result = await oService.Login(oUser);
            if (result)
            {
                message = "You logged in successfully.";
                SuccessMessage(message);
                await Task.Delay(2000);
                oNavigation.NavigateTo("/");
            }
            else
            {
                message = "Wrong credentials, try again.";
                ErrorMessage(message);
            }
            flgBusy = false;
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
