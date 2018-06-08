using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.Identity;
using Sombra.Messaging.Responses.Identity;

namespace Sombra.IdentityService
{
    public class UpdateRolesRequestHandler : IAsyncRequestHandler<UpdateRolesRequest, UpdateRolesResponse>
    {
        private readonly AuthenticationContext _context;

        public UpdateRolesRequestHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task<UpdateRolesResponse> Handle(UpdateRolesRequest message)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (user != null)
            {
                user.Role = message.Role;

                return await _context.TrySaveChangesAsync<UpdateRolesResponse>(response =>
                {
                    response.Role = message.Role;
                });
            }

            return new UpdateRolesResponse
            {
                ErrorType = ErrorType.InvalidKey
            };
        }
    }
}