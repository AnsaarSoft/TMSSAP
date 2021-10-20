using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TMS.Models.Model;
using Microsoft.AspNetCore.Components;

namespace ClientUI.Services
{
    public interface IAPIService
    {
        Task<ResponseMessage> ValidateLogin(User pUser);
    }
    public class APIService : IAPIService
    {
        //private readonly RestSharp.RestClient restclient;
        private readonly HttpClient client;
        private readonly string BaseAddress; 

        public APIService(string pBase)
        {
            BaseAddress = pBase;
            client.BaseAddress = new Uri(BaseAddress);
        }

        public async Task<ResponseMessage> ValidateLogin(User pUser)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                string ApiLink = "api/auth/login";
                var response = await client.PostAsJsonAsync(ApiLink,
                pUser);
                if (response.IsSuccessStatusCode)
                {
                    res.isSuccess = true;
                    res.ErrorMessage = "";
                    res.UserInfo = await response.Content.ReadFromJsonAsync<User>();
                }
                else
                {
                    res.isSuccess = false;
                    res.ErrorMessage = response.ReasonPhrase;
                    res.UserInfo = null;
                }
            }
            catch (Exception ex)
            {
                res.isSuccess = false;
                res.ErrorMessage = ex.Message;
                res.UserInfo = null;
            }
            return res;
        }
    }
}
