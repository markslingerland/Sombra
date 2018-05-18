using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.SearchService.DAL;

namespace Sombra.SearchService
{
    //TODO: change UpdatedCharityEventHandler -> UpdatedCharityActionEventHandler
    public class UpdatedCharityActionEventHandler : IAsyncEventHandler<UpdatedCharityEvent>
    {
        private readonly SearchContext _context;

        public UpdatedCharityActionEventHandler(SearchContext context)
        {
            _context = context;
        }

        public async Task Consume(UpdatedCharityEvent message)
        {
            ExtendedConsole.Log($"UpdatedCharityActionEvent received for charity with key {message.CharityKey}"); //TODO: change to CharityActionKey
            var charityToUpdate = await _context.Content.Where(c => c.Type == Core.Enums.SearchContentType.CharityAction).FirstOrDefaultAsync(u => u.CharityActionKey == message.CharityKey);

            if (charityToUpdate != null)
            {
                charityToUpdate.Name = message.Name;
                charityToUpdate.Description = message.Slogan;
                charityToUpdate.Image = message.CoverImage;
                charityToUpdate.Category = message.Category;               

                await _context.SaveChangesAsync();
            }
        }
    }
}