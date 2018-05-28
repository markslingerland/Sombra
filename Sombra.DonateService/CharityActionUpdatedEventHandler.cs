using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events;
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

        public Task ConsumeAsync(CharityActionUpdatedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}