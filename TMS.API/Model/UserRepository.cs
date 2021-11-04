using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.API.Database;
using TMS.Models.Model;


namespace TMS.API.Model
{
    public interface IUserRepository
    {
        Task<User> ValidateLogin(User oUser);
        Task<User> AddUser(User pUser);
        Task<User> UpdateUser(User pUser);
        Task<List<User>> GetAllUsers();
    }
    public class UserRepository : IUserRepository
    {

        private TMSContext dbContext;

        public UserRepository(TMSContext pContext)
        {
            dbContext = pContext;
        }

        public async Task<User> ValidateLogin(User pUser)
        {
            User rUser = null;
            try
            {
                rUser = await (from a in dbContext.Users
                         where a.UserCode == pUser.UserCode
                         && a.Password == pUser.Password
                         select a).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logs o = new Logs();
                o.Logger(ex);
            }
            return rUser;
        }

        public async Task<User> AddUser(User pUser)
        {
            User rUser = null;
            try
            {
                await dbContext.Users.AddAsync(pUser);
                await dbContext.SaveChangesAsync();
                rUser = pUser;
            }
            catch (Exception ex)
            {
                rUser = null;
                Logs o = new Logs();
                o.Logger(ex);
            }
            return rUser;
        }

        public async Task<User> UpdateUser(User pUser)
        {
            User rUser = null;
            try
            {
                dbContext.Entry<User>(pUser).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                rUser = pUser;
            }
            catch (Exception ex)
            {
                rUser = null;
                Logs o = new Logs();
                o.Logger(ex);
            }
            return rUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> oCollection = new List<User>();
            try
            {
                oCollection = await (from a in dbContext.Users
                               select a).ToListAsync();
            }
            catch (Exception ex)
            {
                oCollection = null;
                Logs o = new Logs();
                o.Logger(ex);
            }
            return oCollection;
        }

    }
}
