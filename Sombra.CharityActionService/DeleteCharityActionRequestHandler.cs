﻿using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using System.Threading.Tasks;
using Sombra.Messaging.Events.CharityAction;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class DeleteCharityActionRequestHandler : IAsyncRequestHandler<DeleteCharityActionRequest, DeleteCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public DeleteCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<DeleteCharityActionResponse> Handle(DeleteCharityActionRequest message)
        {
            var charityAction = await _context.CharityActions.Include(b => b.UserKeys).FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));
            if (charityAction == null)
            {
                return new DeleteCharityActionResponse();
            }

            _context.CharityActions.Remove(charityAction);
            _context.UserKeys.RemoveRange(charityAction.UserKeys);

            if (!await _context.TrySaveChangesAsync()) return new DeleteCharityActionResponse();

            var charityActionDeletedEvent = _mapper.Map<CharityActionDeletedEvent>(charityAction);
            await _bus.PublishAsync(charityActionDeletedEvent);

            return DeleteCharityActionResponse.Success();
        }
    }
}
