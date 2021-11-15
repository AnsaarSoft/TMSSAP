using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.API.Database;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace TMS.API.Model
{
    public interface ITimeSheetRepository
    {
        Task<List<TimeSheet>> GetUserData(vmTimeSheet pTimeSheet);
        Task<TimeSheet> AddTimeSheet(vmAddTime pTime);
        Task<LeaveTime> AddLeave(vmAddTime pTime);
        Task<BreakTime> AddBreak(vmAddTime pTime);
        Task SubmitTimeSheet(vmTimeSheet oSheet);
        Task CancelTimeSheet(vmTimeSheet oSheet);
        Task<List<vmReportSheet>> GetUserReport(DateTime prmFrom, DateTime prmTo, int prmUser, string prmUserName);
        Task<List<vmApprovals>> GetAllApprovals(int prmUserId);
    }

    public class TimeSheetRepository : ITimeSheetRepository
    {
        private TMSContext dbContext;

        public TimeSheetRepository(TMSContext pContext)
        {
            dbContext = pContext;
        }

        public async Task<List<TimeSheet>> GetUserData(vmTimeSheet pTimeSheet)
        {
            List<TimeSheet> oList = new List<TimeSheet>();
            try
            {
                oList = await (from a in dbContext.TimeSheets
                               where a.DayDate >= pTimeSheet.dtFrom
                               && a.DayDate <= pTimeSheet.dtTo
                               && a.rUser == pTimeSheet.oUser.ID
                               select a).ToListAsync();
            }
            catch (Exception ex)
            {
                oList = null;
            }
            return oList;
        }

        public async Task<TimeSheet> AddTimeSheet(vmAddTime pTime)
        {

            try
            {
                TimeSheet oNew = pTime.oTime.Mapping();
                var CheckTime = await (from a in dbContext.TimeSheets
                                       where a.DayDate.Date == oNew.DayDate.Date
                                       && a.rUser == oNew.rUser
                                       select a).FirstOrDefaultAsync();
                if (CheckTime != null && CheckTime.Status != "Cancelled")
                    return null;
                oNew.Status = "Draft";
                await dbContext.TimeSheets.AddAsync(oNew);
                await dbContext.SaveChangesAsync();
                return oNew;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LeaveTime> AddLeave(vmAddTime pTime)
        {

            try
            {
                LeaveTime oNew = pTime.oLeave.Mapping();
                var CheckLeave = await (from a in dbContext.LeaveTimes
                                        where a.rUser == oNew.rUser
                                        && a.rTimeSheet == oNew.rTimeSheet
                                        select a).FirstOrDefaultAsync();
                if (CheckLeave != null)
                    return null;

                await dbContext.LeaveTimes.AddAsync(oNew);
                await dbContext.SaveChangesAsync();
                return oNew;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BreakTime> AddBreak(vmAddTime pTime)
        {

            try
            {
                BreakTime oNew = pTime.oBreak.Mapping();
                var CheckBreak = await (from a in dbContext.BreakTimes
                                        where a.rUser == pTime.oTime.rUser
                                        && a.rTimeSheet == pTime.oTime.ID
                                        select a).FirstOrDefaultAsync();
                if (CheckBreak != null)
                    return null;
                await dbContext.BreakTimes.AddAsync(oNew);
                await dbContext.SaveChangesAsync();
                return oNew;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task SubmitTimeSheet(vmTimeSheet oSheet)
        {
            try
            {
                if (oSheet.oSelected == null)
                    return;
                foreach (var One in oSheet.oSelected)
                {
                    var oRecord = await (from a in dbContext.TimeSheets
                                         where a.ID == One.ID
                                         select a).FirstOrDefaultAsync();
                    if (oRecord == null) continue;
                    if (oRecord.Status == "Draft")
                    {
                        //if(oRecord.flgLeave)
                        //{
                        //    var oLeave = await (from a in dbContext.LeaveTimes
                        //                        where a.rTimeSheet == oRecord.ID
                        //                        && a.rUser == oRecord.rUser
                        //                        select a).FirstOrDefaultAsync();
                        //    if (oLeave != null)
                        //    {
                        //        var oUser = await (from a in dbContext.Users
                        //                           where a.ID == oRecord.rUser
                        //                           select a).FirstOrDefaultAsync();
                        //        if (oUser != null)
                        //        {
                        //            TimeSpan Hours = oLeave.EndTime - oLeave.StartTime;
                        //            oUser.LeaveHours -= Hours.TotalHours;
                        //            dbContext.Entry<User>(oUser).State = EntityState.Modified;
                        //        }
                        //    }

                        //}
                        //oRecord.Status = "Posted";
                        if (oRecord.flgLeave)
                        {
                            var normalUser = await (from a in dbContext.Users
                                                      where a.ID == One.rUser
                                                      select a).FirstOrDefaultAsync();
                            var approvalUser = await (from a in dbContext.Users
                                                      where a.UserName.Contains(normalUser.UserName, StringComparison.InvariantCultureIgnoreCase)
                                                      select a).FirstOrDefaultAsync();
                            UserApproval doc = new();
                            doc.rUser = approvalUser.ID;
                            doc.rDocument = One.ID;
                            doc.Status = "Pending";
                            doc.Remarks = "";
                            doc.UserCode = normalUser.UserName;
                            await dbContext.UserApprovals.AddAsync(doc);
                            oRecord.Status = "Draft-Approval";
                        }
                        else
                        {
                            oRecord.Status = "Posted";
                        }
                    }
                    dbContext.Entry<TimeSheet>(oRecord).State = EntityState.Modified;
                }
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logs logs = new();
                logs.Logger(ex);
            }
        }

        public async Task CancelTimeSheet(vmTimeSheet oSheet)
        {
            try
            {
                if (oSheet.oSelected == null)
                    return;
                foreach (var One in oSheet.oSelected)
                {
                    var oRecord = await (from a in dbContext.TimeSheets
                                         where a.ID == One.ID
                                         select a).FirstOrDefaultAsync();
                    if (oRecord == null) continue;
                    if (oRecord.Status == "Posted") continue;
                    oRecord.Status = "Cancelled";
                    dbContext.Entry<TimeSheet>(oRecord).State = EntityState.Modified;
                }
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<vmReportSheet>> GetUserReport(DateTime prmFrom, DateTime prmTo, int prmUser, string prmUserName)
        {
            List<vmReportSheet> oCollection;
            try
            {
                oCollection = new List<vmReportSheet>();
                var oTimeCollection = await (from a in dbContext.TimeSheets
                                             where a.rUser == prmUser
                                             && a.DayDate.Date >= prmFrom.Date
                                             && a.DayDate.Date <= prmTo.Date
                                             select a).ToListAsync();
                foreach (var Line in oTimeCollection)
                {
                    vmReportSheet oRecord = new vmReportSheet();
                    oRecord.ID = Line.ID;
                    oRecord.UserName = prmUserName;
                    oRecord.DayDate = Line.DayDate.ToString("dd/MM/yyyy");
                    oRecord.StartTime = Line.StartTime.ToString("hh:mm tt");
                    oRecord.EndTime = Line.EndTime.ToString("hh:mm tt");
                    TimeSpan spantime = Line.EndTime - Line.StartTime;
                    oRecord.TotalHour = spantime.TotalHours;
                    oRecord.flgShowBreak = false;
                    oRecord.flgShowLeave = false;

                    oRecord.LeavesList = new List<vmLeaves>();
                    oRecord.BreaksList = new List<vmBreaks>();

                    var oLeaveList = await (from a in dbContext.LeaveTimes
                                            where a.rTimeSheet == Line.ID
                                            select a).ToListAsync();

                    foreach (var LeaveLine in oLeaveList)
                    {
                        vmLeaves oLeave = new vmLeaves();
                        oLeave.StartTime = LeaveLine.StartTime.ToString("hh:mm tt");
                        oLeave.EndTime = LeaveLine.EndTime.ToString("hh:mm tt");
                        TimeSpan span = LeaveLine.EndTime - LeaveLine.StartTime;
                        oLeave.TotalHour = span.TotalHours;
                        oRecord.LeavesList.Add(oLeave);
                    }

                    var oBreakList = await (from a in dbContext.BreakTimes
                                            where a.rTimeSheet == Line.ID
                                            select a).ToListAsync();

                    foreach (var BreakLine in oBreakList)
                    {
                        vmBreaks oBreak = new vmBreaks();
                        oBreak.StartTime = BreakLine.StartTime.ToString("hh:mm tt");
                        oBreak.EndTime = BreakLine.EndTime.ToString("hh:mm tt");
                        TimeSpan span = BreakLine.EndTime - BreakLine.StartTime;
                        oBreak.TotalHour = span.TotalHours;
                        oRecord.BreaksList.Add(oBreak);
                    }
                    oCollection.Add(oRecord);
                }
            }
            catch (Exception ex)
            {
                oCollection = null;
            }
            return oCollection;
        }

        public async Task<List<vmApprovals>> GetAllApprovals(int prmUserId)
        {
            List<vmApprovals> approvalList = new();
            try
            {
                var simpleList = await (from a in dbContext.UserApprovals
                                        join b in dbContext.TimeSheets on a.rDocument equals b.ID
                                        join c in dbContext.Users on a.rUser equals c.ID
                                        join d in dbContext.LeaveTimes on b.ID equals d.rTimeSheet
                                        where a.rUser == prmUserId
                                        && a.Status == "Pending"
                                        select new { ID = a.ID, Username = c.UserName, LeaveDate = b.DayDate.ToString("dd/MM/yyyy"), LeaveStart = d.StartTime.ToString("hh:mm tt"), LeaveEnd = d.EndTime.ToString("hh:mm tt"), d.StartTime, d.EndTime }).ToListAsync();
                foreach (var One in simpleList)
                {
                    vmApprovals doc = new();
                    doc.Username = One.Username;
                    doc.LeaveDate = One.LeaveDate;
                    doc.LeaveStart = One.LeaveStart;
                    doc.LeaveEnd = One.LeaveEnd;
                    doc.Status = "Pending";
                    doc.TotalHour = (One.EndTime - One.StartTime).TotalHours;
                    doc.ID = One.ID;
                    approvalList.Add(doc);
                }
                return approvalList;
            }
            catch (Exception ex)
            {
                Logs logs = new();
                logs.Logger(ex);
                return approvalList;
            }
        }

    }
}

