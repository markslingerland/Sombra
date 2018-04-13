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
using UAParser;

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

        public async Task<bool> ForgotPassword(HttpContext httpContext){
            //Need values: Name, SecretToken, OperatingSystem and used browser.
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

            var userAgentParser = Parser.GetDefault();
            ClientInfo clientInfo = userAgentParser.Parse(userAgent);

            var operatingSystem = clientInfo.OS.Family;
            var browser = clientInfo.UserAgent.Family;

            // var name = _bus.RequestAsync(nameRequest);
            // var secretToken = _bus.RequstAsync(secretTokenRequest)
            
            return false;
        }

        public async Task<bool> SignInAsync(HttpContext httpContext, UserLoginRequest userLoginRequest, bool isPersistent = false)
        {
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
    }
}