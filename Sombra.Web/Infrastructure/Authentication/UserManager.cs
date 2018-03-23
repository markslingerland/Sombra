using System;
using System.Collections.Generic;
using System.Linq;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using System.Security.Claims;
using Sombra.IdentityService;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Sombra.Messaging.Responses;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Web
{
    public class UserManager : IUserManager
    {
        private readonly IBus _bus;

        public UserManager(IBus bus)
        {
            _bus = bus;
        }

        public async Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest)
        {
            return await _bus.RequestAsync(userLoginRequest);
        }

        public async Task<bool> SignIn(HttpContext httpContext, LoginViewModel loginViewModel, bool isPersistent = false)
        {
            var userLoginRequest = new UserLoginRequest(){
                LoginTypeCode = loginViewModel.LoginTypeCode,
                Identifier = loginViewModel.Identifier,
                Secret = SHA256Hasher.ComputeHash(loginViewModel.Secret)
            };

            var userLoginResponse = await ValidateAsync(userLoginRequest);

            if(userLoginResponse.Success){
                ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(userLoginResponse), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
                );
            }

            return userLoginResponse.Success;
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public int GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;

            int currentUserId;

            if (!int.TryParse(claim.Value, out currentUserId))
                return -1;

            return currentUserId;
        }


        private IEnumerable<Claim> GetUserClaims(UserLoginResponse userLoginResponse)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Sid, userLoginResponse.UserKey.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, userLoginResponse.UserName));
            claims.AddRange(this.GetUserRoleClaims(userLoginResponse));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(UserLoginResponse userLoginResponse)
        {
            List<Claim> claims = new List<Claim>();
            IEnumerable<string> permissionCodes = userLoginResponse.PermissionCodes;

            if (permissionCodes != null)
            {
                foreach (var permissionCode in permissionCodes)
                {
                    claims.Add(new Claim(ClaimTypes.Role, permissionCode));
                }
            }

            return claims;
        }
        
        // public User GetCurrentUser(HttpContext httpContext)
        // {
        //     int currentUserId = this.GetCurrentUserId(httpContext);

        //     if (currentUserId == -1)
        //         return null;

        //     return this.context.Users.Find(currentUserId);
        // }
    }
}