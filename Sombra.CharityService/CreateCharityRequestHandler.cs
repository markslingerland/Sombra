using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Events;
using System.Threading.Tasks;

namespace Sombra.CharityService
{
    public class CreateCharityRequestHandler : IAsyncRequestHandler<CreateCharityRequest, CreateCharityResponse>
    {
        private readonly CharityContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateCharityRequestHandler(CharityContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<CreateCharityResponse> Handle(CreateCharityRequest message)
        {
            var charity = _mapper.Map<CharityEntity>(message);
            if (charity.CharityKey == default)
            {
                ExtendedConsole.Log("CreateCharityRequestHandler: CharityKey is empty");
                return new CreateCharityResponse(false);
            }

            _context.Charities.Add(charity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CreateCharityResponse(false);
            }

            var charityCreatedEvent = _mapper.Map<CharityCreatedEvent>(charity);
            await _bus.PublishAsync(charityCreatedEvent);

            return new CreateCharityResponse(true);
        }
    }
}
