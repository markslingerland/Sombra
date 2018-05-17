using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

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

                try
                {
                    await _context.SaveChangesAsync();
                    return new UpdateRolesResponse
                    {
                        Success = true,
                        Role = message.Role
                    };
                }
                catch (DbUpdateException ex)
                {
                    ExtendedConsole.Log(ex);
                    return new UpdateRolesResponse();
                }
            }

            return new UpdateRolesResponse
            {
                ErrorType = ErrorType.InvalidUserKey
            };
        }
    }
}