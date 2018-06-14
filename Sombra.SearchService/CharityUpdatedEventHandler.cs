using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events.Charity;
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

        public async Task ConsumeAsync(CharityUpdatedEvent message)
        {
            var charityToUpdate = await _context.Content.FirstOrDefaultAsync(u => u.CharityKey == message.CharityKey);

            if (charityToUpdate != null)
            {
                charityToUpdate.CharityName = message.Name;
                charityToUpdate.Description = message.Slogan;
                charityToUpdate.Image = message.CoverImage;
                charityToUpdate.Category = message.Category;
                charityToUpdate.Url = message.Url;
                charityToUpdate.Logo = message.Logo;

                await _context.SaveChangesAsync();
            }
        }
    }
}