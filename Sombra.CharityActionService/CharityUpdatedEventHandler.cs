using System.Linq;
using System.Threading.Tasks;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;

namespace Sombra.CharityActionService
{
    public class CharityUpdatedEventHandler : IAsyncEventHandler<CharityUpdatedEvent>
    {
        private readonly CharityActionContext _context;

        public CharityUpdatedEventHandler(CharityActionContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityUpdatedEvent message)
        {
            ExtendedConsole.Log("CharityUpdatedEvent received");
            var actionsToUpdate = _context.CharityActions.Where(a => a.CharityKey == message.CharityKey);
            foreach (var action in actionsToUpdate)
            {
                action.CharityName = message.Name;
            }

            await _context.SaveChangesAsync();
        }
    }
}