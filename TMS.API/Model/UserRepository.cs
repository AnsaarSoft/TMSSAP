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

            }
            return rUser;
        }
    }
}
