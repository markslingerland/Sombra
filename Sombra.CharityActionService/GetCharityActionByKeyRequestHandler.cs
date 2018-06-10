using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sombra.CharityActionService.DAL;
using Sombra.Core;
using System.Threading.Tasks;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests.CharityAction;
using Sombra.Messaging.Responses.CharityAction;

namespace Sombra.CharityActionService
{
    public class GetCharityActionByKeyRequestHandler : IAsyncRequestHandler<GetCharityActionByKeyRequest, GetCharityActionByKeyResponse>
    {
        private readonly CharityActionContext _charityActionContext;
        private readonly IMapper _mapper;

        public GetCharityActionByKeyRequestHandler(CharityActionContext charityActionContext, IMapper mapper)
        {
            _charityActionContext = charityActionContext;
            _mapper = mapper;
        }

        public async Task<GetCharityActionByKeyResponse> Handle(GetCharityActionByKeyRequest message)
        {
            var charityAction = await _charityActionContext.CharityActions.Include(b => b.UserKeys).Include(b => b.Charity).FirstOrDefaultAsync(b => b.CharityActionKey.Equals(message.CharityActionKey));

            return charityAction != null ? _mapper.Map<GetCharityActionByKeyResponse>(charityAction) : new GetCharityActionByKeyResponse();
        }
    }
}