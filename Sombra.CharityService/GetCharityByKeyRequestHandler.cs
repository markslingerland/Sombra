using Sombra.CharityService.DAL;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Sombra.CharityService
{
    public class GetCharityByKeyRequestHandler : IAsyncRequestHandler<CharityRequest, CharityResponse>
    {
        private readonly CharityContext _charityeContext;

        public GetCharityByKeyRequestHandler(CharityContext charityContext)
        {
            _charityeContext = charityContext;
        }

        public async Task<CharityResponse> Handle(CharityRequest message)
        {
            var response = new CharityResponse(true);

            var charity = await _charityeContext.Charity.Where(b => b.CharityId.Equals(message.CharityId)).Select(a => a).FirstOrDefaultAsync();
            if (charity == null) return new CharityResponse(false);

            response.CharityId = charity.CharityId;
            response.NameCharity = charity.NameCharity;
            response.NameOwner = charity.NameOwner;

            return response;
        }
    }
}
