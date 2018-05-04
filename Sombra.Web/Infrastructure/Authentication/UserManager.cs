using System.Collections.Generic;
using System.Linq;
using EasyNetQ;
using System.Security.Claims;
using Sombra.Messaging.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Events;
using AutoMapper;
using Sombra.Web.Areas.Development.Models;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private static string _authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public UserManager(IBus bus, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bus = bus;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest)
        {
            return await _bus.RequestAsync(userLoginRequest);
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel changePasswordViewModel, string securityToken)
        {
            if (!string.IsNullOrEmpty(securityToken))
            {
                if (changePasswordViewModel.Password == changePasswordViewModel.VerifiedPassword)
                {
                    var changePasswordRequest = new ChangePasswordRequest(Core.Encryption.CreateHash(changePasswordViewModel.Password), securityToken);
                    var response = await _bus.RequestAsync(changePasswordRequest);
                    return response.Success;
                }
            }
            return false;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var userAgent = _httpContext.Request.Headers["User-Agent"].ToString();
            var forgotPasswordRequest = new ForgotPasswordRequest(forgotPasswordViewModel.EmailAdress);
            var getUserByEmailRequest = new GetUserByEmailRequest { EmailAddress = forgotPasswordViewModel.EmailAdress };

            var clientInfo = UserAgentParser.Extract(userAgent);
            var user = await _bus.RequestAsync(getUserByEmailRequest);
            var name = $"{user.FirstName} {user.LastName}";
            var forgotPasswordResponse = await _bus.RequestAsync(forgotPasswordRequest);
            var actionurl = $"{_httpContext.Request.Host}/Account/ChangePassword/{forgotPasswordResponse.Secret}";

            var emailTemplateRequest = new EmailTemplateRequest(EmailType.ForgotPassword, TemplateContentBuilder.CreateForgotPasswordTemplateContent(name, actionurl, clientInfo.OperatingSystem, clientInfo.BrowserName));
            var response = await _bus.RequestAsync(emailTemplateRequest);

            var email = new EmailEvent(new EmailAddress("noreply", "noreply@ikdoneer.nu"), new EmailAddress(name, forgotPasswordViewModel.EmailAdress), "Wachtwoord vergeten ikdoneer.nu",
                response.Template, true);

            await _bus.PublishAsync(email);

            return true;
        }

        public async Task<bool> SignInAsync(AuthenticationQuery authenticationQuery, bool isPersistent = false)
        {
            var userLoginRequest = _mapper.Map<UserLoginRequest>(authenticationQuery);
            var userLoginResponse = await ValidateAsync(userLoginRequest);

            if (userLoginResponse.Success){
                await _httpContext.SignInAsync(
                    _authenticationScheme, CreatePrincipal(userLoginResponse), new AuthenticationProperties { IsPersistent = isPersistent }
                );
            }

            return userLoginResponse.Success;
        }

        private SombraPrincipal CreatePrincipal(UserLoginResponse userLoginResponse)
        {
            var identity = new SombraIdentity(GetUserClaims(userLoginResponse), userLoginResponse.UserKey, userLoginResponse.Roles, _authenticationScheme);
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

            claims.AddRange(userLoginResponse.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));

            return claims;
        }
    }
}