using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Core.Enums;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;

namespace Sombra.CharityActionService
{
    public class ApproveCharityActionRequestHandler : IAsyncRequestHandler<ApproveCharityActionRequest, ApproveCharityActionResponse>
    {
        private readonly CharityActionContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public ApproveCharityActionRequestHandler(CharityActionContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<ApproveCharityActionResponse> Handle(ApproveCharityActionRequest message)
        {
            ExtendedConsole.Log("ApproveCharityActionRequest received");
            var charityAction = await _context.CharityActions.FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));
            if (charityAction != null)
            {
                if (charityAction.IsApproved)
                    return new ApproveCharityActionResponse
                    {
                        ErrorType = ErrorType.AlreadyActive
                    };

                charityAction.IsApproved = true;
                await _context.SaveChangesAsync();

                var charityActionCreatedEvent = _mapper.Map<CharityActionCreatedEvent>(charityAction);
                await _bus.PublishAsync(charityActionCreatedEvent);

                return new ApproveCharityActionResponse
                {
                    Success = true
                };
            }

            return new ApproveCharityActionResponse
            {
                ErrorType = ErrorType.NotFound
            };
        }
    }
}