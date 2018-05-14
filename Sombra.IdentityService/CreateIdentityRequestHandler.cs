using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using CredentialType = Sombra.Core.Enums.CredentialType;

namespace Sombra.IdentityService
{
    public class CreateIdentityRequestHandler : IAsyncRequestHandler<CreateIdentityRequest, CreateIdentityResponse>
    {
        private readonly AuthenticationContext _context;

        public CreateIdentityRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<CreateIdentityResponse> Handle(CreateIdentityRequest message)
        {
            if (message.UserKey == default) return new CreateIdentityResponse
            {
                ErrorType = ErrorType.InvalidUserKey
            };

            if (message.CredentialType == CredentialType.Email)
            {
                if (await _context.Credentials.AnyAsync(c => c.CredentialType == CredentialType.Email && c.Identifier.Equals(message.Identifier, StringComparison.OrdinalIgnoreCase)))
                {
                    return new CreateIdentityResponse
                    {
                        ErrorType = ErrorType.EmailExists
                    };
                }
            }

            var activationToken = Hash.SHA256(Guid.NewGuid().ToString());

            var user = new User
            {
                ActivationToken = activationToken,
                Name = message.UserName,
                UserKey = message.UserKey,
                ActivationTokenExpirationDate = DateTime.UtcNow.AddDays(1),
                Role = message.Role
            };

            var credential = new Credential
            {
                Identifier = message.Identifier,
                Secret = message.Secret,
                CredentialType = message.CredentialType,
                User = user
            };

            try
            {
                _context.Users.Add(user);
                _context.Credentials.Add(credential);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CreateIdentityResponse();
            }

            return new CreateIdentityResponse
            {
                ActivationToken = activationToken,
                Success = true
            };
        }
    }
}