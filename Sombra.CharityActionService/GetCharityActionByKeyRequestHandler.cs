using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sombra.CharityActionService
{
    public class GetCharityActionByKeyRequestHandler
    {
        private readonly CharityActionContext _charityeContext;
        private readonly IMapper _mapper;

        public GetCharityActionByKeyRequestHandler(CharityActionContext charityContext, IMapper mapper)
        {
            _charityeContext = charityContext;
            _mapper = mapper;
        }

        public async Task<GetCharityActionResponse> Handle(GetCharityActionRequest message)
        {
            ExtendedConsole.Log("GetCharityActionByKeyRequestHandler received");
            var charityAction = await _charityeContext.CharityActions.Where(b => b.CharityActionkey.Equals(message.CharityActionkey)).Select(a => a).FirstOrDefaultAsync();
            return charityAction != null ? _mapper.Map<GetCharityActionResponse>(charityAction) : new GetCharityActionResponse();
        }
    }
}