using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientUI.Helpers;
using ClientUI.Services;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace ClientUI.Helpers
{
    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAccountServices AccountService { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
            if (authorize && AccountService.oUser == null)
            {
                NavigationManager.NavigateTo($"account/login");
            }
            else
            {
                base.Render(builder);
            }
        }
    }
}
