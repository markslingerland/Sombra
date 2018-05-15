using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Events;
using System.Threading.Tasks;

namespace Sombra.CharityActionService
{
    public class CreateCharityActionRequestHandler : IAsyncRequestHandler<CreateCharityActionRequest, CreateCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<CreateCharityActionResponse> Handle(CreateCharityActionRequest message)
        {
            var charityAction = _mapper.Map<CharityActionEntity>(message);
            if (charityAction.CharityActionkey == default)
            {
                ExtendedConsole.Log("CreateCharityActionRequestHandler: CharityActionKey is empty");
                return new CreateCharityActionResponse(false);
            }

            _context.CharityActions.Add(charityAction);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CreateCharityActionResponse(false);
            }

            var charityActionCreatedEvent = _mapper.Map<CreateCharityActionEvent>(charityAction);
            await _bus.PublishAsync(charityActionCreatedEvent);

            return new CreateCharityActionResponse(true);
        }
    }
}
