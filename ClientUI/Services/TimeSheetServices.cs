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
    public interface ITimeSheetServices
    {
        vmUser oUser { get; }
        Task Initiallize();
        Task<List<TimeSheet>> GetUserTimeSheet(vmTimeSheet oSheet);
        Task<vmAddTime> AddTimeSheet(vmAddTime oTime);
        Task<vmTimeSheet> SubmitTimeSheet(vmTimeSheet oTime);
    }
    public class TimeSheetServices : ITimeSheetServices
    {
        public vmUser oUser { get; private set; }
        private ILocalStorageService oStorageService;
        private IRestClient oClient;

        public TimeSheetServices(ILocalStorageService localStorageService, IRestClient restClient)
        {
            oStorageService = localStorageService;
            oClient = restClient;
            oClient.BaseUrl = new Uri("http://localhost:56388/api/");
        }

        public async Task Initiallize()
        {
            oUser = await oStorageService.GetItemAsync<vmUser>("user");
        }

        public async Task<List<TimeSheet>> GetUserTimeSheet(vmTimeSheet oSheet)
        {
            List<TimeSheet> oCollection = new List<TimeSheet>();
            try
            {
                await Initiallize();
                if (oUser != null)
                {
                    oSheet.oUser = oUser.User;
                }
                else
                {
                    oCollection = null;
                    return oCollection;
                }
                var Request = new RestRequest("/timesheet/getdatabyuser").AddJsonBody(oSheet);
                
                var Response = await oClient.PostAsync<vmTimeSheet>(Request);
                if (Response.flgSuccess)
                {
                    oCollection = Response.oCollection;
                }
                else
                {
                    oCollection = null;
                }
            }
            catch (Exception ex)
            {
                oCollection = null;
            }
            return oCollection;
        }

        public async Task<vmAddTime> AddTimeSheet(vmAddTime oTime)
        {
            try
            {
                await Initiallize();
                if (oUser != null)
                {
                    oTime.oTime.rUser = oUser.User.ID;
                }
                else
                {
                    return null;
                }
                var Request = new RestRequest("/timesheet/addtime").AddJsonBody(oTime);
                var Response = await oClient.PostAsync<vmAddTime>(Request);
                if (Response.flgSuccess)
                {
                    return Response;
                }
                else
                {
                    return Response;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<vmTimeSheet> SubmitTimeSheet(vmTimeSheet oSheet)
        {
            try
            {
                await Initiallize();
                if (oUser != null)
                {
                    oSheet.oUser = oUser.User;
                }
                else
                {
                    return null;
                }
                var Request = new RestRequest("/timesheet/addtime").AddJsonBody(oSheet);
                var Response = await oClient.PostAsync<vmTimeSheet>(Request);
                if (Response.flgSuccess)
                {
                    return Response;
                }
                else
                {
                    return Response;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
