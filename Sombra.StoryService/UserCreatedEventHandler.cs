using System.Threading.Tasks;
using AutoMapper;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class UserCreatedEventHandler : IAsyncEventHandler<UserCreatedEvent>
    {
        private readonly StoryContext _context;
        private readonly IMapper _mapper;

        public UserCreatedEventHandler(StoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ConsumeAsync(UserCreatedEvent message)
        {
            _context.Users.Add(_mapper.Map<User>(message));
            await _context.SaveChangesAsync();
        }
    }
}