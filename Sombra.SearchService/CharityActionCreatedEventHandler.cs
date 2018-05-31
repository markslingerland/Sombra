using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events.CharityAction;

namespace Sombra.SearchService
{
    public class CharityActionCreatedEventHandler : IAsyncEventHandler<CharityActionCreatedEvent>
    {
        private readonly SearchContext _context;

        public CharityActionCreatedEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityActionCreatedEvent message)
        {
            var charityToCreate = new Content()
            {
                CharityName = message.CharityName,
                CharityActionName = message.Name,
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