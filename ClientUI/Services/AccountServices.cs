using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TMS.Models.Model;
using TMS.Models.ViewModel;
using Microsoft.Extensions.Configuration;
using ClientUI.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using ClientUI.Authentication;

namespace ClientUI.Services
{

    public interface IAccountServices
    {
        vmUser oUser { get; }
        Task Initiallize();
        Task<bool> Login(User pUser);
        Task<bool> Logout();
        Task<User> AddUser(User pUser);
        Task<User> UpdateUser(User pUser);
        Task<List<User>> GetAllUser();
        Task<List<string>> GetAllAprovars();
    }

    public class AccountServices : IAccountServices
    {
        public vmUser oUser { get; private set; }
        private ILocalStorageService oStorageService;
        private IRestClient oClient;
        private IConfiguration oConfig;
        private readonly AuthenticationStateProvider oAuth;

        public AccountServices(ILocalStorageService localStorageService, IRestClient restClient, IConfiguration configuration, AuthenticationStateProvider authenticationStateProvider)
        {
            oConfig = configuration;
            oStorageService = localStorageService;
            string value = oConfig.GetValue<string>("APIBase");
            oClient = restClient;
            oClient.UseNewtonsoftJson();
            oClient.BaseUrl = new Uri(value);
            oAuth = authenticationStateProvider;
        }


        public async Task Initiallize()
        {
            try
            {
                oUser = await oStorageService.GetItemAsync<vmUser>("user");
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
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
                    ((AuthStateProvider)oAuth).NotifyUserAuthentication(oUser.JWTKey);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await oStorageService.RemoveItemAsync("user");
                ((AuthStateProvider)oAuth).NotifyUserLogout();
                return true;
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                return false;
            }
        }

        public async Task<User> AddUser(User pUser)
        {
            User oUser;
            try
            {
                var Request = new RestRequest("/account/add").AddJsonBody(pUser);
                var Response = await oClient.PostAsync<User>(Request);
                if (Response != null)
                    oUser = Response;
                else
                    oUser = null;
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                oUser = null;
            }
            return oUser;
        }

        public async Task<User> UpdateUser(User pUser)
        {
            User oUser;
            try
            {
                var Request = new RestRequest("/account/update").AddJsonBody(pUser);
                var Response = await oClient.PostAsync<User>(Request);
                if (Response != null)
                    oUser = Response;
                else
                    oUser = null;
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                oUser = null;
            }
            return oUser;
        }

        public async Task<List<User>> GetAllUser()
        {
            List<User> oList = new List<User>();
            try
            {
                var Request = new RestRequest("/account/getall");
                oList = await oClient.GetAsync<List<User>>(Request);
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
            return oList;
        }

        public async Task<List<string>> GetAllAprovars()
        {
            List<string> oList = new();
            try
            {
                var Request = new RestRequest("/account/getallaprovar");
                var userList = await oClient.GetAsync<List<User>>(Request);
                foreach(var user in userList)
                {
                    string username = user.UserName;
                    oList.Add(username);
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
            return oList;
        }

    }
}
