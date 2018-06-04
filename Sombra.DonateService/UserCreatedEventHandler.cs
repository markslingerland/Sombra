using System.Threading.Tasks;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class UserCreatedEventHandler : IAsyncEventHandler<UserCreatedEvent>
    {
        private readonly DonationsContext _context;

        public UserCreatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(UserCreatedEvent message)
        {
            var userToCreate = new User
            {
                UserKey = message.UserKey,
                UserName = message.FirstName + " " + message.LastName,
                ProfileImage = message.ProfileImage
            };
    
            _context.Users.Add(userToCreate);
            await _context.SaveChangesAsync();
        }
    }
}