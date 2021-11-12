using Blazored.LocalStorage;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.ViewModel;

namespace ClientUI.Shared
{
    public partial class MainLayout
    {

        #region Variable

        [CascadingParameter]
        public Task<AuthenticationState> oAuthState { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }
        [Inject]
        IAccountServices oService { get; set; }
        
        vmUser oUser { get; set; }
        bool flgSideToggle = true;

        #endregion

        #region Functions

        protected override async Task OnInitializedAsync()
        {
            var authstate = await oAuthState;
            if(authstate.User.Identity.IsAuthenticated)
            {
                await oService.Initiallize();
                oUser = oService.oUser;
            }
            if(oUser is null)
            {
                GotoLogin();
            }
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

        #endregion


    }
}
