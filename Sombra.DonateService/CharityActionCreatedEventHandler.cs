using System.Threading.Tasks;
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

        public Task ConsumeAsync(CharityActionCreatedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}