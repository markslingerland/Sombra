using System;
using System.Threading.Tasks;
using AutoMapper;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class CharityCreatedEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;
        

        public CharityCreatedEventHandler(DonationsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }

        public async Task ConsumeAsync(CharityCreatedEvent message)
        {
           _context.Add(_mapper.Map<Charity>(message));
            await _context.SaveChangesAsync();
        }
    }
}