using System.Threading.Tasks;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Infrastructure;
using System;
using Microsoft.EntityFrameworkCore;
using CredentialType = Sombra.Core.Enums.CredentialType;

namespace Sombra.IdentityService
{
    public class ForgotPasswordRequestHandler : IAsyncRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
    {
        private readonly AuthenticationContext _context;

        public ForgotPasswordRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest message)
        {
            ExtendedConsole.Log("ForgotPasswordRequest received");
            var response = new ForgotPasswordResponse();

            var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.Identifier == message.Email && c.CredentialType == CredentialType.Email && c.User.IsActive);

            if (credential != null)
            {
                var guid = Guid.NewGuid().ToString();
                var securityToken = Hash.SHA256(guid);
                credential.SecurityToken = securityToken;
                credential.ExpirationDate = DateTime.UtcNow.AddDays(1);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Secret = securityToken;
            }

            return response;
        }
    }
}