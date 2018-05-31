using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events.Charity;

namespace Sombra.SearchService
{
    public class CharityCreatedEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly SearchContext _context;

        public CharityCreatedEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityCreatedEvent message)
        {
            ExtendedConsole.Log($"CreatedCharityEvent received for charity with key {message.CharityKey}");
            var charityToCreate = new Content()
            {
                CharityName = message.Name,
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