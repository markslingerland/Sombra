using System.Threading.Tasks;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using System;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService
{
    public class UserLoginRequestHandler : AsyncCrudRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly AuthenticationContext _context;
        public UserLoginRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public override async Task<UserLoginResponse> Handle(UserLoginRequest message)
        {
            var credential = await _context.Credentials.Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CredentialType == message.LoginTypeCode && c.Identifier.Equals(message.Identifier, StringComparison.OrdinalIgnoreCase));

            if (credential != null)
            {
                if (!credential.User.IsActive)
                {
                    return Error(ErrorType.InactiveAccount);
                }

                return new UserLoginResponse
                {
                    IsSuccess = true,
                    UserKey = credential.User.UserKey,
                    UserName = credential.User.Name,
                    Role = credential.User.Role,
                    EncrytedPassword = credential.Secret
                };
            }

            return Error(ErrorType.InvalidPassword);
        }
    }
}