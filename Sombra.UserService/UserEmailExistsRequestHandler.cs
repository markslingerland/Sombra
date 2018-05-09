using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core.Extensions;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.UserService.DAL;

namespace Sombra.UserService
{
    public class UserEmailExistsRequestHandler : IAsyncRequestHandler<UserEmailExistsRequest, UserEmailExistsResponse>
    {
        private readonly UserContext _context;

        public UserEmailExistsRequestHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<UserEmailExistsResponse> Handle(UserEmailExistsRequest message)
        {
            Expression<Func<User, bool>> filter = u => u.EmailAddress.Equals(message.EmailAddress, StringComparison.OrdinalIgnoreCase);
            if (message.CurrentUserKey != default)
                filter = filter.And(u => u.UserKey != message.CurrentUserKey);

            return new UserEmailExistsResponse
            {
                EmailExists = await _context.Users.AnyAsync(filter)
            };
        }
    }
}