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
            ExtendedConsole.Log($"CreatedCharityEvent received for charity with key {message.CharityKey}");
            var charityToCreate = new Content()
            {
                Name = message.Name,
                Type = Core.Enums.SearchContentType.Charity,
                Key = message.CharityKey,
                Image = message.CoverImage,
                Description = message.Slogan,
                Category = message.Category                              
            };
    
            _context.Add(charityToCreate);
            await _context.SaveChangesAsync();
        }
    }
}