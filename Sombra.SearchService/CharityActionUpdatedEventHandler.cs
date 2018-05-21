using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService
{
    public class CharityActionUpdatedEventHandler : IAsyncEventHandler<CharityActionUpdatedEvent>
    {
        private readonly SearchContext _context;

        public CharityActionUpdatedEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(CharityActionUpdatedEvent message)
        {
            ExtendedConsole.Log($"UpdatedCharityActionEvent received for charity with key {message.CharityActionKey}");
            var charityToUpdate = await _context.Content
                .Where(c => c.Type == Core.Enums.SearchContentType.CharityAction)
                .FirstOrDefaultAsync(u => u.CharityActionKey == message.CharityActionKey);

            if (charityToUpdate != null)
            {
                charityToUpdate.Name = message.Name;
                charityToUpdate.Description = message.Description;
                charityToUpdate.Image = message.CoverImage;
                charityToUpdate.Category = message.Category;

                await _context.SaveChangesAsync();
            }
        }
    }
}