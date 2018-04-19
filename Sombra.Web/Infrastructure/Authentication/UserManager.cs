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

namespace Sombra.Web.Infrastructure.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly IBus _bus;
        private readonly HttpContext _httpContext;
        private static string _authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public UserManager(IBus bus, IHttpContextAccessor httpContextAccessor)
        {
            _bus = bus;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<bool> ValidateAsync(UserLoginRequest userLoginRequest)
        {
            return (await _bus.RequestAsync(userLoginRequest)).Success;
        }

        public async Task<bool> SignInAsync(UserLoginRequest userLoginRequest, bool isPersistent = false)
        {
            var userLoginResponse = await _bus.RequestAsync(userLoginRequest);

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