using System.Threading.Tasks;
using AutoMapper;
using Sombra.CharityActionService.DAL;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;

namespace Sombra.CharityActionService
{
    public class CharityCreatedEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;

        public CharityCreatedEventHandler(CharityActionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ConsumeAsync(CharityCreatedEvent message)
        {
            _context.Charities.Add(_mapper.Map<Charity>(message));
            await _context.SaveChangesAsync();
        }
    }
}