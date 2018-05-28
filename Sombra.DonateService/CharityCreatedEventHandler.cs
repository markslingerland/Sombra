using System;
using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events;
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

        public Task ConsumeAsync(CharityCreatedEvent message)
        {
            throw new NotImplementedException();
        }
    }
}