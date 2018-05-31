using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sombra.Core.Enums;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;
using UserKey = Sombra.CharityActionService.DAL.UserKey;

namespace Sombra.CharityActionService
{
    public class UpdateCharityActionRequestHandler : IAsyncRequestHandler<UpdateCharityActionRequest, UpdateCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<UpdateCharityActionResponse> Handle(UpdateCharityActionRequest message)
        {
            var charityAction = await _context.CharityActions.Include(b => b.UserKeys).FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));

            if (charityAction == null)
            {
                return new UpdateCharityActionResponse
                {
                    ErrorType = ErrorType.NotFound
                };
            }

            _context.Entry(charityAction).CurrentValues.SetValues(message);
            _context.UserKeys.RemoveRange(charityAction.UserKeys);
            var mappedKeys = _mapper.Map<List<UserKey>>(message.UserKeys);
            _context.UserKeys.AddRange(mappedKeys);
            charityAction.UserKeys = mappedKeys;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new UpdateCharityActionResponse();
            }

            var charityActionUpdatedEvent = _mapper.Map<CharityActionUpdatedEvent>(charityAction);
            await _bus.PublishAsync(charityActionUpdatedEvent);

            return new UpdateCharityActionResponse
            {
                Success = true
            };
        }
    }
}