﻿using Microsoft.EntityFrameworkCore;
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
                               && a.rUser.ID == pTimeSheet.oUser.ID
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
                var CheckTime = await (from a in dbContext.TimeSheets
                                       where a.DayDate == pTime.oTime.DayDate
                                       && a.rUser.ID == pTime.oTime.rUser.ID
                                       select a).FirstOrDefaultAsync();
                if (CheckTime != null)
                    return null;

                TimeSheet oNew = new TimeSheet()
                {
                    DayDate = pTime.oTime.DayDate,
                    StartTime = pTime.oTime.StartTime,
                    EndTime = pTime.oTime.EndTime,
                    flgLeave = pTime.oTime.flgLeave,
                    flgBreak = pTime.oTime.flgBreak,
                    Status = "Draft",
                    rUser = null
                };
                
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
                var CheckLeave = await (from a in dbContext.LeaveTimes
                                       where a.rUser.ID == pTime.oTime.rUser.ID
                                       && a.rTimeSheet.ID == pTime.oTime.ID
                                       select a).FirstOrDefaultAsync();
                if (CheckLeave != null)
                    return null;

                LeaveTime oNew = pTime.oLeave;
                oNew.rTimeSheet = pTime.oTime;
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
                var CheckBreak = await (from a in dbContext.BreakTimes
                                        where a.rUser.ID == pTime.oTime.rUser.ID
                                        && a.rTimeSheet.ID == pTime.oTime.ID
                                        select a).FirstOrDefaultAsync();
                if (CheckBreak != null)
                    return null;

                BreakTime oNew = pTime.oBreak;
                oNew.rTimeSheet = pTime.oTime;
                await dbContext.BreakTimes.AddAsync(oNew);
                await dbContext.SaveChangesAsync();
                return oNew;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}

