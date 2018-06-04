using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityActionCreatedEventHandler : IAsyncEventHandler<CharityActionCreatedEvent>
    {
        private readonly DonationsContext _context;

        public CharityActionCreatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityActionCreatedEvent message)
        {
            var charity = await _context.Charities.FirstOrDefaultAsync(c => c.CharityKey == message.CharityKey);
            if (charity != null)
            {
                var charityActionToCreate = new CharityAction()
                {
                    CharityActionKey = message.CharityActionKey,
                    ActionEndDateTime = message.ActionEndDateTime,
                    Name = message.Name                           
                };             

                charity.ChartityActions.Add(charityActionToCreate);
            }
            await _context.SaveChangesAsync();
        }
    }
}