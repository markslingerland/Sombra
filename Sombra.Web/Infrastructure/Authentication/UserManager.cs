using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyNetQ;
using System.Security.Claims;
using Sombra.Messaging.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Infrastructure;
using UAParser;
using Sombra.Messaging.Events;
using AutoMapper;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.Models;
using Sombra.Web.Infrastructure;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly IBus _bus;
<<<<<<< HEAD
        private readonly HttpContext _httpContext;
        private static string _authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public UserManager(IBus bus, IHttpContextAccessor httpContextAccessor)
        {
            _bus = bus;
            _httpContext = httpContextAccessor.HttpContext;
=======
        private readonly IMapper _mapper;

        public UserManager(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
>>>>>>> master
        }

        public async Task<bool> ValidateAsync(UserLoginRequest userLoginRequest)
        {
            return (await _bus.RequestAsync(userLoginRequest)).Success;
        }

<<<<<<< HEAD

        public async Task<bool> ForgotPassword(HttpContext httpContext){
            //Need values: Name, SecretToken, OperatingSystem and used browser.
=======
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
>>>>>>> master
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
<<<<<<< HEAD
            var userLoginResponse = await _bus.RequestAsync(userLoginRequest);
=======
            var userLoginRequest = _mapper.Map<UserLoginRequest>(authenticationQuery);

            var userLoginResponse = await ValidateAsync(userLoginRequest);

            if (userLoginResponse.Success)
            {
                ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(userLoginResponse), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
>>>>>>> master

            if (userLoginResponse.Success){
                await _httpContext.SignInAsync(
                    _authenticationScheme, CreatePrincipal(userLoginResponse), new AuthenticationProperties { IsPersistent = isPersistent }
                );
            }

            return userLoginResponse.Success;
        }

        private SombraPrincipal CreatePrincipal(UserLoginResponse userLoginResponse)
        {
            var identity = new SombraIdentity(GetUserClaims(userLoginResponse), userLoginResponse.UserKey, userLoginResponse.Roles, userLoginResponse.Permissions, _authenticationScheme);
            return new SombraPrincipal(identity);
        }

        public async Task SignOut()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IEnumerable<Claim> GetUserClaims(UserLoginResponse userLoginResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userLoginResponse.UserKey.ToString()),
                new Claim(ClaimTypes.Name, userLoginResponse.UserName)
            };

            claims.AddRange(GetUserRoleClaims(userLoginResponse));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(UserLoginResponse userLoginResponse)
        {
            var claims = new List<Claim>();

            if (userLoginResponse.Permissions != null)
            {
                claims.AddRange(userLoginResponse.Permissions.Select(permission => new Claim(ClaimTypes.Role, permission.ToString())));
            }

            return claims;
        }
    }
}