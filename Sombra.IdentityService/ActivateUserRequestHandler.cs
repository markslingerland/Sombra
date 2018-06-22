using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService
{
    public class ActivateUserRequestHandler : AsyncCrudRequestHandler<ActivateUserRequest, ActivateUserResponse>
    {
        private readonly AuthenticationContext _context;

        public ActivateUserRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public override async Task<ActivateUserResponse> Handle(ActivateUserRequest message)
        {
            if (string.IsNullOrEmpty(message.ActivationToken)) return Error(ErrorType.TokenInvalid);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ActivationToken.Equals(message.ActivationToken));
            if (user != null)
            {
                if (DateTime.UtcNow > user.ActivationTokenExpirationDate) return Error(ErrorType.TokenExpired);

                user.IsActive = true;
                user.ActivationToken = null;

                return await _context.TrySaveChangesAsync<ActivateUserResponse>();
            }

            return Error(ErrorType.TokenInvalid);
        }
    }
}
