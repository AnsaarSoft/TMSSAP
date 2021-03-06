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

namespace ClientUI.Services
{
    public interface ITimeSheetServices
    {
        vmUser oUser { get; }
        Task Initiallize();
        Task<List<TimeSheet>> GetUserTimeSheet(vmTimeSheet oSheet);
        Task<vmAddTime> AddTimeSheet(vmAddTime oTime);
        Task<vmTimeSheet> SubmitTimeSheet(vmTimeSheet oSheet);
        Task<vmTimeSheet> CancelTimeSheet(vmTimeSheet oSheet);
        Task<List<vmReportSheet>> GetUserReport(string prmFrom, string prmTo, int prmUserId, string prmUserName);
        Task<List<vmApprovals>> GetAllPendingDocument();
        Task<bool> ApproveTimesheet(vmApprovals prmTimesheet);
        Task<bool> RejectTimesheet(vmApprovals prmTimesheet);
    }
    public class TimeSheetServices : ITimeSheetServices
    {
        public vmUser oUser { get; private set; }
        private ILocalStorageService oStorageService;
        private IRestClient oClient;
        private IConfiguration oConfig;

        public TimeSheetServices(ILocalStorageService localStorageService, IRestClient restClient, IConfiguration configuration)
        {
            oConfig = configuration;
            string value = oConfig.GetValue<string>("APIBase");
            oStorageService = localStorageService;
            oClient = restClient;
            oClient.UseNewtonsoftJson();
            oClient.BaseUrl = new Uri(value);
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
                
                var Response = await oClient.PostAsync<List<TimeSheet>>(Request);
                if (Response.Count > 0)
                {
                    oCollection = Response;
                }
                else
                {
                    oCollection = null;
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
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
                Logs.Logger(ex);
                return null;
            }
        }

        public async Task<vmTimeSheet> SendToApproval(vmTimeSheet oSheet)
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
                var Request = new RestRequest("/timesheet/submitsheet").AddJsonBody(oSheet);
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
                Logs.Logger(ex);
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
                
                var Request = new RestRequest("/timesheet/submitsheet").AddJsonBody(oSheet);
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
                Logs.Logger(ex);
                return null;
            }
        }

        public async Task<vmTimeSheet> CancelTimeSheet(vmTimeSheet oSheet)
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
                var Request = new RestRequest("/timesheet/cancelsheet").AddJsonBody(oSheet);
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
                Logs.Logger(ex);
                return null;
            }
        }

        public async Task<List<vmReportSheet>> GetUserReport(string prmFrom, string prmTo, int prmUserId, string prmUserName)
        {
            try
            {
                var Request = new RestRequest("/timesheet/getuserreport")
                    .AddParameter("prmFrom", prmFrom)
                    .AddParameter("prmTo", prmTo)
                    .AddParameter("prmUser", prmUserId)
                    .AddParameter("prmUserName", prmUserName);
                List<vmReportSheet> Response = await oClient.GetAsync<List<vmReportSheet>>(Request);
                if (Response != null)
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
                Logs.Logger(ex);
                return null;
            }
        }

        public async Task<List<vmApprovals>> GetAllPendingDocument()
        {
            List<vmApprovals> approvalList = new();
            try
            {
                await Initiallize();
                if (oUser is null)
                {
                    return null;
                }
                var Request = new RestRequest("/timesheet/getallapproval")
                    .AddParameter("prmUser", oUser.User.ID);
                approvalList = await oClient.GetAsync<List<vmApprovals>>(Request);
                //if(approvalList.Count > 0)
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
            }
            return approvalList;
        }

        public async Task<bool> ApproveTimesheet(vmApprovals prmTimesheet)
        {
            try
            {
                var Request = new RestRequest("/timesheet/approvesheet").AddJsonBody(prmTimesheet);
                var value = await oClient.PostAsync<vmApprovals>(Request);
                if(value is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                return false;
            }
        }

        public async Task<bool> RejectTimesheet(vmApprovals prmTimesheet)
        {
            try
            {
                var Request = new RestRequest("/timesheet/rejectsheet").AddJsonBody(prmTimesheet);
                var value = await oClient.PostAsync<vmApprovals>(Request);
                if(value is not null)
                {
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

    }
}
