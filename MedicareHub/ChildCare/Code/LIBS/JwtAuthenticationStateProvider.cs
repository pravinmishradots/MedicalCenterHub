using ChildCare.Services;
using ChildCareApi.Models;
using ChildCareCore.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ChildCare.Code.LIBS
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {

        private AuthUser? authUser;
        public JwtAuthenticationStateProvider(AuthUser _authUser)
        {
            authUser = _authUser;

        }

        private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var defaultAuthStateTask = GetAuthenticationStateAsyncCore();
            return defaultAuthStateTask;

            async Task<AuthenticationState> GetAuthenticationStateAsyncCore()
            {
                //await SetAuthUserAndGet();

                currentUser = new ClaimsPrincipal(new ClaimsIdentity());

                if (authUser != null && authUser.Id != Guid.Empty)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                   

                        new Claim(ClaimTypes.Name,ClaimTypes.SerialNumber, authUser.Id.ToString())
                }, "JwtAuth");

                    currentUser = new ClaimsPrincipal(identity);
                }

                return new AuthenticationState(currentUser);
            }

        }

        public Task LogInAsync(AuthUser user, bool notify = false)
        {
            var loginTask = LogInAsyncCore();

            if (notify) NotifyAuthenticationStateChanged(loginTask);

            return loginTask;


            async Task<AuthenticationState> LogInAsyncCore()
            {
                currentUser = new ClaimsPrincipal(new ClaimsIdentity());

                if (user != null)
                {
                  
                    //await SetAuthUserAndGet();

                    var identity = new ClaimsIdentity(new[]
                    {
                     new Claim(ClaimTypes.Name,ClaimTypes.SerialNumber, user.Id.ToString())
                }, "JwtAuth");

                    currentUser = new ClaimsPrincipal(identity);
                }

                return new AuthenticationState(currentUser);
            }
        }

        public Task LogoutAsync()
        {
            var logoutTask = LogOutAsyncCore();

            NotifyAuthenticationStateChanged(logoutTask);

            return logoutTask;

            async Task<AuthenticationState> LogOutAsyncCore()
            {
         

                currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(currentUser);
            }
        }

        //private async Task<AuthUser?> SetAuthUserAndGet()
        //{
            
          
          
        //    if (authUser != null)
        //    {
        //        authUser.Id = CurrentUser
        //        authUser.Name = user.Name;
        //        authUser.AccessToken = user.;
        //        authUser.LastName = user.LastName;
        //        authUser.PhoneNumber = user.PhoneNumber;
        //        authUser.AccessToken = user.AccessToken;
        //        authUser.UserType = user.UserType;
        //        authUser.CountryCode = user.CountryCode;
        //        authUser.ReferralID = user.ReferralID;
        //        authUser.Roles = user.Roles;
        //        authUser.BusinessId = user.BusinessId;
        //        authUser.PrimaryUserName = user.PrimaryUserName;
        //        authUser.AccreditedStatus = user.AccreditedStatus;
        //        authUser.Title = user.Title;
        //        authUser.IsBusinessInfoRequired = user.IsBusinessInfoRequired;
        //    }
        //    return authUser;
        //}

    }
}












