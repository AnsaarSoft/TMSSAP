using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        Task<List<User>> GetAllAprovar();
        Task<string> GenerateToken(User user);
    }
    public class UserRepository : IUserRepository
    {

        private TMSContext dbContext;
        private IConfiguration oConfig;

        public UserRepository(TMSContext pContext, IConfiguration configuration)
        {
            dbContext = pContext;
            oConfig = configuration;
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
                if (rUser == null) return null;

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

        public async Task<List<User>> GetAllAprovar()
        {
            List<User> oCollection = new List<User>();
            try
            {
                oCollection = await (from a in dbContext.Users
                                     where a.flgAprover == true
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

        public async Task<string> GenerateToken(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString())
                };

                //Role Admin, User
                if(user.flgSuper)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }

                var Token = new JwtSecurityToken(
                    new JwtHeader(
                        new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(oConfig.GetValue<string>("MySecretKey"))), SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims)
                    );

                string TokenString = "";
                TokenString = new JwtSecurityTokenHandler().WriteToken(Token);
                return TokenString;
            }
            catch (Exception ex)
            {
                Logs logger = new Logs();
                logger.Logger(ex);
                return "";
            }
        }

    }
}
