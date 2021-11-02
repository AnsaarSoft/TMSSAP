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
        Task<User> AddUser(User pUser);
        Task<User> UpdateUser(User pUser);
        Task<List<User>> GetAllUser();
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
            }
            return oList;
        }

    }
}
