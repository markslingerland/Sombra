using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sombra.Core;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;
using Sombra.StoryService.DAL;

namespace Sombra.StoryService
{
    public class UserUpdatedEventHandler : IAsyncEventHandler<UserUpdatedEvent>
    {
        private readonly StoryContext _context;

        public UserUpdatedEventHandler(StoryContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(UserUpdatedEvent message)
        {
            var userToUpdate = await _context.Users.SingleOrDefaultAsync(u => u.UserKey == message.UserKey);
            if (userToUpdate != null)
            {
                userToUpdate.Name = Helpers.GetUserName(message);
                userToUpdate.ProfileImage = message.ProfileImage;
                await _context.SaveChangesAsync();
            }
        }
    }
}