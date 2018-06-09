using System.Linq;
using System.Threading.Tasks;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class CharityUpdatedEventHandler : IAsyncEventHandler<CharityUpdatedEvent>
    {
        private readonly StoryContext _context;

        public CharityUpdatedEventHandler(StoryContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityUpdatedEvent message)
        {
            var storiesToUpdate = _context.Stories.Where(a => a.CharityKey == message.CharityKey);
            foreach (var story in storiesToUpdate)
            {
                story.CharityName = message.Name;
            }

            await _context.SaveChangesAsync();
        }
    }
}