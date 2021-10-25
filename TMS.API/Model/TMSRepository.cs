using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.API.Database;
using TMS.Models.Model;

namespace TMS.API.Model
{
    public interface ITMSRepository
    {
        Task<User> GetLogin(User user);
    }

    public class TMSRepository : ITMSRepository
    {
        private readonly TMSContext appdb;
        public TMSRepository(TMSContext context)
        {
            appdb = context;
        }

        public async Task<User> GetLogin(User user)
        {
            User oUser = new User();
            await Task.Run(() =>
            {
                oUser = new User();
            });
            return oUser;
        }
    }
}
