using System.Threading.Tasks;
using AutoMapper;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class UserCreatedEventHandler : IAsyncEventHandler<UserCreatedEvent>
    {
        private readonly DonationsContext _context;
        private readonly IMapper _mapper;

        public UserCreatedEventHandler(DonationsContext context, IMapper mapper)
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