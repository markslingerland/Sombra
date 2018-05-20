using System.Linq;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Events;
using Sombra.SearchService.DAL;
using Sombra.Core;

namespace Sombra.SearchService
{
    public class CreatedCharityActionEventHandler : IAsyncEventHandler<CharityActionCreatedEvent>
    {
        private readonly SearchContext _context;

        public CreatedCharityActionEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(CharityActionCreatedEvent message)
        {
            ExtendedConsole.Log($"CreatedCharityActionEvent received for charity with key {message.CharityActionKey}");
            var charityToCreate = new Content()
            {
                Name = message.NameAction,
                Type = Core.Enums.SearchContentType.CharityAction,
                CharityActionKey = message.CharityActionKey,
                CharityKey = message.CharityKey,
                Image = message.CoverImage,
                Description = message.Description,
                Category = message.Category                              
            };
    
            _context.Add(charityToCreate);
            await _context.SaveChangesAsync();
        }
    }
}