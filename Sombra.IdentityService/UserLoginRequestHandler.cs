using System.Threading.Tasks;
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
            Console.WriteLine("UserLoginRequest received");
            var response = new UserLoginResponse();

            var credential = await _context.Credentials.Include(c => c.User.UserRoles).ThenInclude(ur => ur.Role.RolePermissions).ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(c => c.CredentialType.Name.ToString().ToLower() == message.LoginTypeCode.ToString().ToLower() && c.Identifier.ToLower() == message.Identifier.ToLower());

            if (credential != null && Encryption.ValidatePassword(message.Secret, credential.Secret))
            {
                response.Success = true;
                response.UserKey = credential.User.UserKey;
                response.UserName = credential.User.Name;
                response.Roles = credential.User.UserRoles.Select(r => r.Role.Name).ToList();
                response.Permissions = credential.User.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.Name)).ToList();
            }

            return response;

        }
    }
}