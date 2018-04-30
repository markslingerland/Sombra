using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Role = Sombra.Core.Enums.Role;

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
            var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (user != null)
            {
                _context.UserRoles.RemoveRange(user.UserRoles);
                var response = new UpdateRolesResponse
                {
                    Success = true,
                    Roles = new List<Role>()
                };

                foreach (var role in message.Roles)
                {
                    var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
                    if (existingRole != null)
                    {
                        _context.UserRoles.Add(new UserRole
                        {
                            Role = existingRole,
                            User = user
                        });
                        response.Roles.Add(role);
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ExtendedConsole.Log(ex);
                    return new UpdateRolesResponse();
                }

                return response;
            }

            return new UpdateRolesResponse
            {
                ErrorType = ErrorType.InvalidUserKey
            };
        }
    }
}