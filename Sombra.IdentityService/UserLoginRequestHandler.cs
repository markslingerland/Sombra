using System.Threading.Tasks;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using System;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

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
            ExtendedConsole.Log("UserLoginRequest received");
            var response = new UserLoginResponse();

            var credential = await _context.Credentials.Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CredentialType == message.LoginTypeCode && c.Identifier.Equals(message.Identifier, StringComparison.OrdinalIgnoreCase));

            if (credential != null && Encryption.ValidatePassword(message.Secret, credential.Secret))
            {
                if (!credential.User.IsActive)
                {
                    response.ErrorType = ErrorType.InactiveAccount;
                }
                else
                {
                    response.Success = true;
                    response.UserKey = credential.User.UserKey;
                    response.UserName = credential.User.Name;
                    response.Role = credential.User.Role;
                }
            }
            else
            {
                response.ErrorType = ErrorType.InvalidPassword;
            }

            return response;
        }
    }
}