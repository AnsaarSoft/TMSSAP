using Blazored.LocalStorage;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.ViewModel;

namespace ClientUI.Shared
{
    public partial class MainLayout
    {
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        IAccountServices oService { get; set; }
        
        vmUser oUser { get; set; }
        bool flgSideToggle = true;

        protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
        {
            await oService.Initiallize();
            oUser = oService.oUser;
            if (oUser == null)
                GotoLogin();
            return base.OnAfterRenderAsync(firstRender);
        }



        void DrawToggle()
        {
            flgSideToggle = !flgSideToggle;
        }

        void GotoLogin()
        {
            oNavigation.NavigateTo("/account/login");
        }

        public async Task LogOut()
        {
            await oService.Logout();
            oNavigation.NavigateTo("/account/login");
        }


    }
}
