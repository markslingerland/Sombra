using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
using Sombra.Messaging.Events;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sombra.CharityService
{
    public class UpdateCharityRequestHandler : IAsyncRequestHandler<CharityRequest, CharityResponse>
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

        public async Task<CharityResponse> Handle(CharityRequest message)
        {

            ExtendedConsole.Log("ChangeCharityRequest received");
            var charity = await _context.Charities.FirstOrDefaultAsync(u => u.CharityId == message.CharityId);
            if (charity == null)
            {
                return new CharityResponse
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
                return new CharityResponse
                {
                    Success = false
                };
            }

            var userUpdatedEvent = _mapper.Map<CharityCreatedEvent>(charity);
            await _bus.PublishAsync(userUpdatedEvent);

            return new CharityResponse
            {
                Success = true
            };
        }
    }
}