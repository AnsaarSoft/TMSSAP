using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientUI.Data
{
    public class TokenManager
    {
        private static string SecretKey = "";

        public static string GenerateToken(string prmUser)
        {
            byte[] key = Convert.FromBase64String(SecretKey);
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, prmUser)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler myHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken myToken = myHandler.CreateJwtSecurityToken(descriptor);
            return myHandler.WriteToken(myToken);

        }

        public static ClaimsPrincipal GetPrinciple(string prmToken)
        {
            try
            {
                JwtSecurityTokenHandler oHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken oToken = (JwtSecurityToken) oHandler.ReadToken(prmToken);
                if (oToken == null) return null;
                byte[] key = Convert.FromBase64String(SecretKey);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal claimsPrincipal = oHandler.ValidateToken(prmToken, parameters, out securityToken);
                return claimsPrincipal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ValidateToken(string prmUser)
        {
            string UserName = null;

            ClaimsPrincipal claimsPrincipal = GetPrinciple(prmUser);
            if (claimsPrincipal == null) return null;
            ClaimsIdentity claimsIdentity;
            try
            {
                claimsIdentity = (ClaimsIdentity)claimsPrincipal.Identity;
            }
            catch (Exception)
            {
                return null;
            }
            Claim claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            UserName = claim.Value;

            return UserName;
        }
    }
}
