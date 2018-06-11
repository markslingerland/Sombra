using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;
using CredentialType = Sombra.Core.Enums.CredentialType;

namespace Sombra.IdentityService
{
    public class GetUserActivationTokenRequestHandler : IAsyncRequestHandler<GetUserActivationTokenRequest, GetUserActivationTokenResponse>
    {
        private readonly AuthenticationContext _context;

        public GetUserActivationTokenRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<GetUserActivationTokenResponse> Handle(GetUserActivationTokenRequest message)
        {
            if (string.IsNullOrEmpty(message.EmailAddress))
                return new GetUserActivationTokenResponse
                {
                    ErrorType = ErrorType.InvalidEmail
                };

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Credentials.Any(c => c.Identifier.Equals(message.EmailAddress, StringComparison.OrdinalIgnoreCase) && c.CredentialType == CredentialType.Email));

            if (user != null)
            {
                if (user.IsActive)
                    return new GetUserActivationTokenResponse
                    {
                        ErrorType = ErrorType.AlreadyActive
                    };

                user.ActivationToken = Hash.SHA256(Guid.NewGuid().ToString());
                user.ActivationTokenExpirationDate = DateTime.UtcNow.AddDays(1);

                return await _context.TrySaveChangesAsync()
                    ? new GetUserActivationTokenResponse
                    {
                        ActivationToken = user.ActivationToken,
                        UserName = user.Name
                    }
                    : new GetUserActivationTokenResponse();
            }

            return new GetUserActivationTokenResponse
            {
                ErrorType = ErrorType.InvalidEmail
            };
        }
    }
}