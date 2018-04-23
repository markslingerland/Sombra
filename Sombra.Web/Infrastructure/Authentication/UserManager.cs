using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using System.Security.Claims;
using Sombra.Messaging.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Sombra.Messaging.Responses;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using UAParser;
using Sombra.Messaging.Events;
using AutoMapper;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.Models;
using Sombra.Web.Infrastructure;
using Sombra.Web.ViewModels;

namespace Sombra.Web
{
    public class UserManager : IUserManager
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public UserManager(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest)
        {
            return await _bus.RequestAsync(userLoginRequest);
        }

        public async Task<bool> ChangePassword(HttpContext httpContext, ChangePasswordViewModel changePasswordViewModel, string securityToken)
        {
            if (!string.IsNullOrEmpty(securityToken)) { 
                if(changePasswordViewModel.Password == changePasswordViewModel.VerifiedPassword){
                    var changePasswordRequest = new ChangePasswordRequest(Core.Encryption.CreateHash(changePasswordViewModel.Password), securityToken);
                    var response = await _bus.RequestAsync(changePasswordRequest);
                    return response.Success;
                }
            }
            return false;
        }
        
        public async Task<bool> ForgotPassword(HttpContext httpContext, ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var forgotPasswordRequest = new ForgotPasswordRequest(forgotPasswordViewModel.EmailAdress);
            var getUserByEmailRequest = new GetUserByEmailRequest{ EmailAddress = forgotPasswordViewModel.EmailAdress };

            var clientInfo = UserAgentParser.Extract(userAgent);

            var operatingSystem = clientInfo.OperatingSystem;
            var browserName = clientInfo.BrowserName;

            var user = await _bus.RequestAsync(getUserByEmailRequest);
            var name = $"{user.FirstName} {user.LastName}";
            var forgotPasswordResponse = await _bus.RequestAsync(forgotPasswordRequest);
            var actionurl = $"{httpContext.Request.Host}/Account/ChangePassword/{forgotPasswordResponse.Secret.ToString()}";

            var emailTemplateRequest = new EmailTemplateRequest(EmailType.ForgotPasswordTemplate, TemplateContentBuilder.CreateForgotPasswordTempleteContent(name, actionurl, operatingSystem, browserName));
            var response = await _bus.RequestAsync(emailTemplateRequest);

            var email = new EmailEvent(new EmailAddress("noreply", "noreply@ikdoneer.nu"), new EmailAddress(name, forgotPasswordViewModel.EmailAdress), "Wachtwoord vergeten ikdoneer.nu",
                                                    response.Template, true);

            await _bus.PublishAsync(email);

            return true;
        }

        public async Task<bool> SignInAsync(HttpContext httpContext, AuthenticationQuery authenticationQuery, bool isPersistent = false)
        {
            var userLoginRequest = _mapper.Map<UserLoginRequest>(authenticationQuery);

            var userLoginResponse = await ValidateAsync(userLoginRequest);

            if (userLoginResponse.Success)
            {
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