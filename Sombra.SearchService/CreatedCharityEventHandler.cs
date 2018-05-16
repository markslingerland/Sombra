using System.Linq;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Events;
using Sombra.SearchService.DAL;
using Sombra.Core;

namespace Sombra.SearchService
{
    public class CreatedCharityEventHandler : IAsyncEventHandler<CreatedCharityEvent>
    {
        private readonly SearchContext _context;

        public CreatedCharityEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(CreatedCharityEvent message)
        {
            ExtendedConsole.Log($"CreatedCharityEvent received for charity with key {message.CharityId}"); //TODO: change CharityId to Key when ready
            var charityToCreate = new Content()
            {
                Name = message.NameCharity,
                Type = Core.Enums.SearchContentType.Charity
                //TODO: add key.   
                //TODO: add image.
                //TODO: add description.    
                
            };
    
            _context.Add(charityToCreate);
            await _context.SaveChangesAsync();
        }
    }
}