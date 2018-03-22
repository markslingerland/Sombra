using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Sombra.Messaging;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Infrastructure;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Sombra.IdentityService
{
    public class UserLoginRequestHandler : IAsyncRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly AuthenticationContext _context;
        public UserLoginRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest message)
        {
            var response = new UserLoginResponse();

            var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role.RolePermissions).ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Credentials.Any(c => c.CredentialType.Code.ToLower() == message.LoginTypeCode && c.Identifier.ToLower() == message.Identifier && c.Secret == message.Secret)).ConfigureAwait(false);

            if (user != null)          
            {
                response.Success = true;
                response.UserKey = user.UserKey;
                response.UserName = user.Name;
                response.PermissionCodes = user.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.Code));
            }

            return response;

        }
    }
}