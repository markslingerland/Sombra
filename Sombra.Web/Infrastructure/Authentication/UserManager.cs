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

namespace Sombra.Web
{
    public class UserManager : IUserManager
    {
        private AuthenticationContext context;
        private readonly IBus _bus;

        public UserManager(IBus bus, AuthenticationContext context)
        {
        _bus = bus;
        this.context = context;
        }

        public async Task<bool> ValidateAsync(string loginTypeCode, string identifier, string secret)
        {
            var result = false;

            var userLoginRequest = new UserLoginRequest(){
                LoginTypeCode = loginTypeCode,
                Identifier = identifier,
                Secret = SHA256Hasher.ComputeHash(secret)
            };

            var response = await _bus.RequestAsync<UserLoginRequest, UserLoginResponse>(userLoginRequest);

            if(response.Success){
                //SignIn(HttpContext.Current, response)
                result = true;
            }

            return result;
        }

        public async void SignIn(HttpContext httpContext, User user, bool isPersistent = false)
        {
        ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
        );
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

        public User GetCurrentUser(HttpContext httpContext)
        {
        int currentUserId = this.GetCurrentUserId(httpContext);

        if (currentUserId == -1)
            return null;

        return this.context.Users.Find(currentUserId);
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
        List<Claim> claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.Name));
        claims.AddRange(this.GetUserRoleClaims(user));
        return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(User user)
        {
        List<Claim> claims = new List<Claim>();
        IEnumerable<Guid> roleIds = this.context.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).ToList();

        if (roleIds != null)
        {
            foreach (var roleId in roleIds)
            {
            Role role = this.context.Roles.Find(roleId);

            claims.Add(new Claim(ClaimTypes.Role, role.Code));
            claims.AddRange(this.GetUserPermissionClaims(role));
            }
        }

        return claims;
        }

        private IEnumerable<Claim> GetUserPermissionClaims(Role role)
        {
        List<Claim> claims = new List<Claim>();
        IEnumerable<Guid> permissionIds = this.context.RolePermissions.Where(rp => rp.RoleId == role.Id).Select(rp => rp.PermissionId).ToList();

        if (permissionIds != null)
        {
            foreach (var permissionId in permissionIds)
            {
            Permission permission = this.context.Permissions.Find(permissionId);

            claims.Add(new Claim("Permission", permission.Code));
            }
        }

        return claims;
        }
    }
}