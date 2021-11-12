using Blazored.LocalStorage;
using ClientUI.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMS.Models.Model;
using TMS.Models.ViewModel;

namespace ClientUI.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IRestClient oClient;
        private readonly ILocalStorageService oService;
        private readonly AuthenticationState oAnonymus;

        public AuthStateProvider(IRestClient restClient, ILocalStorageService localStorageService)
        {
            oClient = restClient;
            oService = localStorageService;
            oAnonymus = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                vmUser user = await oService.GetItemAsync<vmUser>("user");
                if(user is not null)
                {
                    string jwtTokenString = user.JWTKey;
                    if(string.IsNullOrEmpty(jwtTokenString))
                    {
                        return oAnonymus;
                    }

                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(jwtTokenString), "jwtAuthType")));
                }
                else
                {
                    return oAnonymus;
                }
            }
            catch (Exception ex)
            {
                Logs.Logger(ex);
                return oAnonymus;
            }
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(oAnonymus);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
