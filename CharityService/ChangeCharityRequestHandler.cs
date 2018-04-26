using Microsoft.EntityFrameworkCore;
using Sombra.CharityService.DAL;
using Sombra.Core;
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
    class ChangeCharityRequestHandler : IAsyncRequestHandler<CharityRequest, CharityResponse>
    {
        private readonly CharityContext _context;
        public ChangeCharityRequestHandler(CharityContext context)
        {
            _context = context;
        }

        public async Task<CharityResponse> Handle(CharityRequest message)
        {

            ExtendedConsole.Log("ChangeCharityRequest received");
            var response = new CharityResponse(false);

            var charity = await _context.Charity.Where(b => b.CharityId.Equals(message.CharityId)).Select(a => a).FirstOrDefaultAsync();
            if (charity != null)
            {
                charity.NameCharity = message.NameCharity;
                charity.NameOwner = message.NameOwner;
                _context.SaveChanges();
                response.Success = true;
            }

            return response;
        }
    }
}