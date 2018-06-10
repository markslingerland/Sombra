using System.Threading.Tasks;
using AutoMapper;
using Sombra.Messaging.Events.Charity;
using Sombra.Messaging.Infrastructure;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class CharityCreatedEventHandler : IAsyncEventHandler<CharityCreatedEvent>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public CharityCreatedEventHandler(StoryContext context, IMapper mapper)
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