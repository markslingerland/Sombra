using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
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
            var charityToUpdate = await _context.Charities.FirstOrDefaultAsync(a => a.CharityKey == message.CharityKey);
            if (charityToUpdate != null)
            {
                _context.Entry(charityToUpdate).CurrentValues.SetValues(message);
            }

            await _context.SaveChangesAsync();
        }
    }
}