using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Sombra.CharityActionService
{
    public class GetCharityActionByKeyRequestHandler
    {
        private readonly CharityActionContext _charityActionContext;
        private readonly IMapper _mapper;

        public GetCharityActionByKeyRequestHandler(CharityActionContext charityActionContext, IMapper mapper)
        {
            _charityActionContext = charityActionContext;
            _mapper = mapper;
        }

        public async Task<GetCharityActionResponse> Handle(GetCharityActionRequest message)
        {
            ExtendedConsole.Log("GetCharityActionByKeyRequestHandler received");
            var charityAction = await _charityActionContext.CharityActions.Where(b => b.CharityActionkey.Equals(message.CharityActionkey)).Select(a => a).FirstOrDefaultAsync();
            return charityAction != null ? _mapper.Map<GetCharityActionResponse>(charityAction) : new GetCharityActionResponse();
        }
    }
}