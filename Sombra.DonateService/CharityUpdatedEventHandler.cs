using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityUpdatedEventHandler : IAsyncEventHandler<CharityUpdatedEvent>
    {
        private readonly DonationsContext _context;

        public CharityUpdatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public Task ConsumeAsync(CharityUpdatedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}