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
using AutoMapper;
using Sombra.Core;

namespace Sombra.CharityService
{
    public class GetCharityByKeyRequestHandler : IAsyncRequestHandler<GetCharityRequest, GetCharityResponse>
    {
        private readonly CharityContext _charityeContext;
        private readonly IMapper _mapper;

        public GetCharityByKeyRequestHandler(CharityContext charityContext, IMapper mapper)
        {
            _charityeContext = charityContext;
            _mapper = mapper;
        }

        public async Task<GetCharityResponse> Handle(GetCharityRequest message)
        {
            ExtendedConsole.Log("GetCharityByKeyRequestHandler received");
            var charity = await _charityeContext.Charities.Where(b => b.CharityId.Equals(message.CharityId)).Select(a => a).FirstOrDefaultAsync();
            return charity != null ? _mapper.Map<GetCharityResponse>(charity) : new GetCharityResponse();
        }
    }
}