using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sombra.CharityService
{
    class CreateCharityRequestHandler : IAsyncRequestHandler<CharityRequest, CharityResponse>
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

        public async Task<CharityResponse> Handle(CharityRequest message)
        {
            var charity = _mapper.Map<CharityEntity>(message);
            if (charity.CharityId == default)
            {
                ExtendedConsole.Log("CreateUserRequestHandler: UserKey is empty");
                return new CharityResponse(false);
            }

            _context.Charity.Add(charity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ExtendedConsole.Log(ex);
                return new CharityResponse(false);
            }

            var charityCreatedEvent = _mapper.Map<CharityCreatedEvent>(charity);
            await _bus.PublishAsync(charityCreatedEvent);

            return new CharityResponse(true);
        }
    }
}
