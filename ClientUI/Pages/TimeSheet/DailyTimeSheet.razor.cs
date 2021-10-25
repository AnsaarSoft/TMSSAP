using ClientUI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.ViewModel;

namespace ClientUI.Pages.TimeSheet
{
    public partial class DailyTimeSheet
    {
        #region Variable

        DateTime? FromDate = DateTime.Now;
        DateTime? ToDate = DateTime.Now;
        bool flgBusy = false;

        vmTimeSheet oModel = new vmTimeSheet();

        [Inject]
        ITimeSheetServices oService { get; set; }
        [Inject]
        NavigationManager oNavigation { get; set; }

        #endregion

        #region Function

        public async Task GetData()
        {
            flgBusy = true;
            try
            {
                oModel.dtFrom = FromDate.GetValueOrDefault();
                oModel.dtTo = ToDate.GetValueOrDefault();
                oModel.oCollection = await oService.GetUserTimeSheet(oModel);

            }
            catch (Exception ex)
            {

            }
            flgBusy = false;
        }

        public void WeekStart()
        {
            try
            {
                DateTime start = DateTime.Now;  
                DateTime end = DateTime.Now;
                while(start.DayOfWeek != DayOfWeek.Sunday)
                {
                    start = start.AddDays(-1);
                }
                
                while (end.DayOfWeek != DayOfWeek.Saturday)
                {
                    end = end.AddDays(1);
                }
                FromDate = start;
                ToDate = end;
            }
            catch (Exception)
            {

            }
        }

        public async Task GotoTimeSheetAdd()
        {
            oNavigation.NavigateTo("/timesheet/addtime");
        }

        #endregion
    }
}
