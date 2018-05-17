using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService
{
    public class UpdatedCharityEventHandler : IAsyncEventHandler<UpdatedCharityEvent>
    {
        private readonly SearchContext _context;

        public UpdatedCharityEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(UpdatedCharityEvent message)
        {
            ExtendedConsole.Log($"UpdatedCharityEvent received for charity with key {message.CharityKey}");
            var charityToUpdate = await _context.Content.FirstOrDefaultAsync(u => u.Key == message.CharityKey);

            if (charityToUpdate != null)
            {
                charityToUpdate.Name = message.Name;
                charityToUpdate.Description = message.Slogan;
                charityToUpdate.Image = message.CoverImage;
                //TODO: add catogories

                await _context.SaveChangesAsync();
            }
        }
    }
}