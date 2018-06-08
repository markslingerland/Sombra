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
    public class ChangePasswordRequestHandler : IAsyncRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly AuthenticationContext _context;

        public ChangePasswordRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest message)
        {
            var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.SecurityToken == message.SecurityToken && c.User.IsActive);

            if (credential != null)
            {
                if (credential.ExpirationDate > DateTime.UtcNow)
                {
                    credential.Secret = message.Password;
                    credential.SecurityToken = string.Empty;
                    return await _context.TrySaveChangesAsync<ChangePasswordResponse>();
                }

                return new ChangePasswordResponse
                {
                    ErrorType = ErrorType.TokenExpired
                };
            }

            return new ChangePasswordResponse
            {
                ErrorType = ErrorType.TokenInvalid
            };
        }
    }
}