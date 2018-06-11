using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class CharityUpdatedEventHandler : IAsyncEventHandler<CharityUpdatedEvent>
    {
        private readonly StoryContext _context;

        public CharityUpdatedEventHandler(StoryContext context)
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