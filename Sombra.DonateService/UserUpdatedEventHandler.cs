using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.DonateService.DAL;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;

namespace Sombra.DonateService
{
    public class UserUpdatedEventHandler : IAsyncEventHandler<UserUpdatedEvent>
    {
        private readonly DonationsContext _context;

        public UserUpdatedEventHandler(DonationsContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(UserUpdatedEvent message)
        {
            var userToUpdate = await _context.Users.SingleOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (userToUpdate != null)
            {
                userToUpdate.UserName = Helpers.GetUserName(message);
                userToUpdate.ProfileImage = message.ProfileImage;
                await _context.SaveChangesAsync();
            }
        }
    }
}