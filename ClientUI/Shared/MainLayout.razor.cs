using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientUI.Shared
{
    public partial class MainLayout
    {
        [Inject]
        NavigationManager oNavigation { get; set; }
        
        bool flgSideToggle = true;

        void DrawToggle()
        {
            flgSideToggle = !flgSideToggle;
        }

        void GotoLogin()
        {
            oNavigation.NavigateTo("/account/login");
        }
    }
}
