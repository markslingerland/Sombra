using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityActionCreatedEventHandler : IAsyncEventHandler<CharityActionCreatedEvent>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public CharityActionCreatedEventHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ConsumeAsync(CharityActionCreatedEvent message)
        {
            var charity = await _context.Charities.FirstOrDefaultAsync(c => c.CharityKey == message.CharityKey);
            if (charity != null)
            {
                charity.ChartityActions.Add(_mapper.Map<CharityAction>(message));
            }
            await _context.SaveChangesAsync();
        }
    }
}