using Blazored.LocalStorage;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
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
        bool isBusy = false;
        string message = string.Empty;

        [Inject]
        IAccountServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        
        #endregion

        #region Functions

        public async Task LoginUser()
        {
            isBusy = true;
            var result = await oService.Login(oUser);
            if (result)
            {
                message = "You logged in successfully.";
                ShowSuccess(message);
                await Task.Delay(2000);
                oNavigation.NavigateTo("/");
            }
            else
            {
                message = "Wrong credentials, try again.";
                ShowError(message);
            }
            isBusy = false;
        }

        void ShowSuccess(string message)
        {
            
        }

        void ShowError(string message)
        {
        }

        #endregion
    }
}
