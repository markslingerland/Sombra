using System.Threading.Tasks;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Infrastructure;
using System;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;

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
            ExtendedConsole.Log("ChangePasswordRequest received");

            var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.SecurityToken == message.SecurityToken && c.User.IsActive);

            if (credential != null)
            {
                if (credential.ExpirationDate > DateTime.UtcNow)
                {
                    credential.Secret = message.Password;
                    credential.SecurityToken = string.Empty;
                    await _context.SaveChangesAsync();
                    return new ChangePasswordResponse
                    {
                        Success = true
                    };
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