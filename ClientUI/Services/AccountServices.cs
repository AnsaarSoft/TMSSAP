using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace ClientUI.Services
{

    public interface IAccountServices
    {
        vmUser oUser { get; }
        Task Initiallize();
        Task<bool> Login(User pUser);
        Task<bool> Logout();
    }

    public class AccountServices : IAccountServices
    {
        public vmUser oUser { get; private set; }
        private ILocalStorageService oStorageService;
        private IRestClient oClient;

        public AccountServices(ILocalStorageService localStorageService, IRestClient restClient)
        {
            oStorageService = localStorageService;
            oClient = restClient;
            oClient.BaseUrl = new Uri("http://localhost:56388/api/");
        }


        public async Task Initiallize()
        {
            oUser = await oStorageService.GetItemAsync<vmUser>("user");
        }

        public async Task<bool> Login(User pUser)
        {
            try
            {
                var Request = new RestRequest("/account/login").AddJsonBody(pUser);
                var Response = await oClient.PostAsync<ResponseMessage>(Request);
                if (Response.isSuccess)
                {
                    vmUser oUser = new vmUser();
                    oUser.User = Response.UserInfo;
                    oUser.JWTKey = Response.JWTKey;
                    await oStorageService.SetItemAsync("user", oUser);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await oStorageService.RemoveItemAsync("user");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
