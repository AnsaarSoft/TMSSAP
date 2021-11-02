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

        string usercode { get; set; } = "";
        string username { get; set; } = "";
        string password { get; set; } = "";

        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

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

        public async Task GetAllUser()
        {
            try
            {
                oCollection = await oService.GetAllUser();
            }
            catch (Exception)
            {
                ErrorMessage("Something went wrong.");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAllUser();
            //return base.OnInitializedAsync();
        }

        private void SelectedRow(TableRowClickEventArgs<User> tableRowClickEventArgs)
        {
            oModel = tableRowClickEventArgs.Item;
        }

        #endregion
    }
}
