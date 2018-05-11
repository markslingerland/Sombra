using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Infrastructure.Authentication
{
    public class AuthenticationManager
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private static string _authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public AuthenticationManager(IBus bus, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bus = bus;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<UserLoginResponse> ValidateAsync(UserLoginRequest userLoginRequest)
        {
            return await _bus.RequestAsync(userLoginRequest);
        }

        public async Task<LoginResultViewModel> SignInAsync(LoginViewModel authenticationQuery, bool isPersistent = false)
        {
            var userLoginRequest = _mapper.Map<UserLoginRequest>(authenticationQuery);
            var userLoginResponse = await ValidateAsync(userLoginRequest);

            if (userLoginResponse.Success)
            {
                await _httpContext.SignInAsync(
                    _authenticationScheme, CreatePrincipal(userLoginResponse), new AuthenticationProperties { IsPersistent = isPersistent }
                );
            }

            return new LoginResultViewModel { Success = userLoginResponse.Success };
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