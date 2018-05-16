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
            ExtendedConsole.Log($"UpdatedCharityEvent received for charity with key {message.CharityId}"); //TODO: change CharityId to Key when ready
            var charityToUpdate = await _context.Content.FirstOrDefaultAsync(u => u.Key == System.Guid.Parse(message.CharityId)); //TODO: change CharityId to Key when ready

            if (charityToUpdate != null)
            {
                charityToUpdate.Name = message.NameCharity;
                //TODO: add description
                //TODO: add coverImage
                //TODO: add catogories

                await _context.SaveChangesAsync();
            }
        }
    }
}