using System;
using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityCreatedEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly DonationsContext _context;

        public CharityCreatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(CharityCreatedEvent message)
        {
            var charityToCreate = new Charity()
            {
                CharityKey = message.CharityKey,
                Name = message.Name                               
            };
    
            _context.Add(charityToCreate);
            await _context.SaveChangesAsync();
        }
    }
}