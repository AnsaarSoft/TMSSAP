using ClientUI.Helpers;
using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.Model;

namespace ClientUI.Pages.Account
{
    public partial class UserList
    {

        #region Variable

        bool flgBusy = false;
        User oModel = new User();
        List<User> oCollection = new List<User>();
        List<string> oAprovarList = new();
        string SelectedAprovar;
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        private List<BreadcrumbItem> oBreadList = new List<BreadcrumbItem>
        {
            new BreadcrumbItem ("Home", href:"/"),
            new BreadcrumbItem ("User List", href:"/account/userlist")
        };

        [Inject]
        IAccountServices oService { get; set; }
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

        public async Task SaveUser()
        {
            flgBusy = true;
            try
            {
                if (string.IsNullOrEmpty(oModel.UserCode))
                    return;
                if (oModel.ID > 0)
                {
                    var UpdatedUser = await oService.UpdateUser(oModel);
                    if(UpdatedUser != null)
                    {
                        SuccessMessage("Record updated successfully.");
                    }
                }
                else
                {
                    var oNewUser = await oService.AddUser(oModel);
                    if (oNewUser != null)
                    {
                        oCollection.Add(oNewUser);
                    }
                    SuccessMessage("Record added successfully.");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage("Something went wrong.");
                Logs.Logger(ex);
            }
            flgBusy = false;
        }

        public async Task CancelUser()
        {
            oNavigation.NavigateTo("/");
        }

        void PasswordShowToggle()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        public async Task FormInitiallize()
        {
            try
            {
                oCollection = await oService.GetAllUser();
                oAprovarList = await oService.GetAllAprovars();
            }
            catch (Exception ex)
            {
                ErrorMessage("Something went wrong.");
                Logs.Logger(ex);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await FormInitiallize();
            //return base.OnInitializedAsync();
        }

        private void SelectedRow(TableRowClickEventArgs<User> tableRowClickEventArgs)
        {
            oModel = tableRowClickEventArgs.Item;
        }

        public async Task<IEnumerable<string>> SearchAprovar(string selected)
        {
            try
            {
                await Task.Delay(5);
                if (string.IsNullOrEmpty(selected))
                    return oAprovarList;
                return oAprovarList.Where(a => a.Contains(selected, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage("Something went wrong.");
                Logs.Logger(ex);
                return oAprovarList;
            }

        }

        #endregion
    }
}
