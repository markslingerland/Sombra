using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityUpdatedEventHandler : IAsyncEventHandler<CharityUpdatedEvent>
    {
        private readonly DonationsContext _context;

        public CharityUpdatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityUpdatedEvent message)
        {
            var charityToUpdate = await _context.Charities
                .FirstOrDefaultAsync(c => c.CharityKey == message.CharityKey);

            if (charityToUpdate != null)
            {
                _context.Entry(charityToUpdate).CurrentValues.SetValues(message);
                
                await _context.SaveChangesAsync();
            }
        }
    }
}