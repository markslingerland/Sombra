using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityActionUpdatedEventHandler : IAsyncEventHandler<CharityActionUpdatedEvent>
    {
        private readonly DonationsContext _context;

        public CharityActionUpdatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityActionUpdatedEvent message)
        {
            var charityActionToUpdate = await _context.CharityActions
                .FirstOrDefaultAsync(c => c.CharityActionKey == message.CharityActionKey);

            if (charityActionToUpdate != null)
            {
                charityActionToUpdate.ActionEndDateTime = message.ActionEndDateTime;
                charityActionToUpdate.CharityActionKey = message.CharityActionKey;
                charityActionToUpdate.Name = message.Name;
                charityActionToUpdate.Image = message.CoverImage;
                charityActionToUpdate.ThankYou = message.ThankYou;    
                
                await _context.SaveChangesAsync();
            }
        }
    }
}