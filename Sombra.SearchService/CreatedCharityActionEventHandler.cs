using System.Linq;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Events;
using Sombra.SearchService.DAL;
using Sombra.Core;

namespace Sombra.SearchService
{
    //TODO: CHANGE CharityCreatedEvent -> CharityActionCreatedEvent
    public class CreatedCharityActionEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly SearchContext _context;

        public CreatedCharityActionEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(CharityCreatedEvent message)
        {
            ExtendedConsole.Log($"CreatedCharityActionEvent received for charity with key {message.CharityKey}");
            var charityToCreate = new Content()
            {
                Name = message.Name,
                Type = Core.Enums.SearchContentType.Charity,
                CharityKey = message.CharityKey,
                Image = message.CoverImage,
                Description = message.Slogan,
                Category = message.Category                              
            };
    
            _context.Add(charityToCreate);
            await _context.SaveChangesAsync();
        }
    }
}