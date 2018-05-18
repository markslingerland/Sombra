using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            ExtendedConsole.Log("UpdateCharityActionRequest received");
            var charityAction = await _context.CharityActions.Include(b => b.UserKeys).FirstOrDefaultAsync(b => b.CharityActionkey.Equals(message.CharityActionkey));

            if (charityAction == null)
            {
                return new UpdateCharityActionResponse();
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