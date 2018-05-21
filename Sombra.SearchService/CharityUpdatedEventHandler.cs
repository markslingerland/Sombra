using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService
{
    public class CharityUpdatedEventHandler : IAsyncEventHandler<CharityUpdatedEvent>
    {
        private readonly SearchContext _context;

        public CharityUpdatedEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(CharityUpdatedEvent message)
        {
            ExtendedConsole.Log($"UpdatedCharityEvent received for charity with key {message.CharityKey}");
            var charityToUpdate = await _context.Content.FirstOrDefaultAsync(u => u.CharityKey == message.CharityKey);

            if (charityToUpdate != null)
            {
                charityToUpdate.Name = message.Name;
                charityToUpdate.Description = message.Slogan;
                charityToUpdate.Image = message.CoverImage;
                charityToUpdate.Category = message.Category; 
                                
                await _context.SaveChangesAsync();
            }
        }
    }
}