using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.IdentityService.DAL;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using CredentialType = Sombra.Core.Enums.CredentialType;

namespace Sombra.IdentityService
{
    public class UserUpdatedEventHandler : IAsyncEventHandler<UserUpdatedEvent>
    {
        private readonly AuthenticationContext _context;

        public UserUpdatedEventHandler(AuthenticationContext context)
        {
            _context = context;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            ExtendedConsole.Log($"UserUpdatedEvent received for user with key {message.UserKey}");
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserKey == message.UserKey);

            if (userToUpdate != null)
            {
                userToUpdate.Name = $"{message.FirstName} {message.LastName}";

                var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.UserId == userToUpdate.Id && c.CredentialType == CredentialType.Email);
                if (credential != null) credential.Identifier = message.EmailAddress;

                await _context.SaveChangesAsync();
            }
        }
    }
}