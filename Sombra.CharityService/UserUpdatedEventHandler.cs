using System.Linq;
using System.Threading.Tasks;
using Sombra.Core;
using Sombra.CharityService.DAL;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;

namespace Sombra.CharityService
{
    public class UserUpdatedEventHandler : IAsyncEventHandler<UserUpdatedEvent>
    {
        private readonly CharityContext _context;

        public UserUpdatedEventHandler(CharityContext context)
        {
            _context = context;
        }

        public async Task Consume(UserUpdatedEvent message)
        {
            ExtendedConsole.Log($"UserUpdatedEvent received for user with key {message.UserKey}");
            var charitiesToUpdate = _context.Charities.Where(u => u.OwnerUserKey == message.UserKey);

            foreach (var charity in charitiesToUpdate)
            {
                charity.OwnerUserName = $"{message.FirstName} {message.LastName}";
            }

            await _context.SaveChangesAsync();
        }
    }
}