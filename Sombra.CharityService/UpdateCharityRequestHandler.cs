using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Threading.Tasks;

namespace Sombra.CharityService
{
    public class UpdateCharityRequestHandler : IAsyncRequestHandler<UpdateCharityRequest, UpdateCharityResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateCharityRequestHandler(CharityContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<UpdateCharityResponse> Handle(UpdateCharityRequest message)
        {

            ExtendedConsole.Log("UpdateCharityRequest received");
            var charity = await _context.Charities.FirstOrDefaultAsync(u => u.CharityId == message.CharityId);
            if (charity == null)
            {
                return new UpdateCharityResponse
                {
                    Success = false,
                };
            }

            _context.Entry(charity).CurrentValues.SetValues(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new UpdateCharityResponse
                {
                    Success = false
                };
            }

            var userUpdatedEvent = _mapper.Map<UpdateCharityEvent>(charity);
            await _bus.PublishAsync(userUpdatedEvent);

            return new UpdateCharityResponse
            {
                Success = true
            };
        }
    }
}