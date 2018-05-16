using System.Linq;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Events;
using Sombra.SearchService.DAL;
using Sombra.Core;

namespace Sombra.SearchService
{
    public class CreatedCharityEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly SearchContext _context;

        public CreatedCharityEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(CharityCreatedEvent message)
        {
            ExtendedConsole.Log($"CreatedCharityEvent received for charity with key {message.CharityKey}"); //TODO: change CharityId to Key when ready
            var charityToCreate = new Content()
            {
                Name = message.Name,
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