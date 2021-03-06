﻿using System.Linq;
using System.Threading.Tasks;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events.User;
using Sombra.Messaging.Infrastructure;

namespace Sombra.CharityActionService
{
    public class UserUpdatedEventHandler : IAsyncEventHandler<UserUpdatedEvent>
    {
        private readonly CharityActionContext _context;

        public UserUpdatedEventHandler(CharityActionContext context)
        {
            _context = context;
        }

        public async Task ConsumeAsync(UserUpdatedEvent message)
        {
            var charitieActionsToUpdate = _context.CharityActions.Where(u => u.OrganiserUserKey == message.UserKey);

            foreach (var charityAction in charitieActionsToUpdate)
            {
                charityAction.OrganiserUserName = Helpers.GetUserName(message);
                charityAction.OrganiserImage = message.ProfileImage;
            }

            await _context.SaveChangesAsync();
        }
    }
}